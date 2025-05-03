using Customers.Management.Application.Interfaces;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Domain.Exceptions;
using Customers.Management.Domain.Messages;
using Customers.Management.Infra.Repositories;
using MassTransit;

namespace Customers.Management.Application.Services;

internal class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private IPublishEndpoint _publishEndpoint;

    public CustomerService(
        ICustomerRepository customerRepository,
        IPublishEndpoint publishEndpoint)
    {
        _customerRepository = customerRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        var customersResponse = customers.ToResponse();

        return customersResponse;
    }

    public async Task<CustomerResponse> GetCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
        if (customer == null)
            throw new DomainException("Cliente não encontrado.");

        var customerResponse = customer.ToResponse();

        return customerResponse;
    }

    public async Task<CustomerResponse> InsertCustomerAsync(CustomerRequest request, CancellationToken cancellationToken)
    {
        if (request?.Id != Guid.Empty)
            throw new DomainException("Id informado no corpo da requisição.");

        var customerExist = await _customerRepository.GetByTaxIdAsync(request.TaxId!, cancellationToken);
        if (customerExist != null)
            throw new DomainException("CPF já consta na base de dados.");

        var customer = request.ToEntity();

        await _customerRepository.InsertAsync(customer, cancellationToken);
        await _customerRepository.CommitAsync();
        await PublishZipCodeMessageAsync(request.ZipCode!, cancellationToken);

        var customerResponse = customer.ToResponse();

        return customerResponse;
    }

    public async Task<CustomerResponse> UpdateCustomerAsync(CustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
            throw new DomainException("Cliente não encontrado.");

        customer.UpdateFrom(request);

        await _customerRepository.Update(customer, cancellationToken);
        await _customerRepository.CommitAsync();
        await PublishZipCodeMessageAsync(customer.ZipCode, cancellationToken);

        var customerResponse = customer.ToResponse();

        return customerResponse;
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
        if (customer == null)
            throw new DomainException("Cliente não encontrado.");

        await _customerRepository.DeleteAsync(customer, cancellationToken);
        await _customerRepository.CommitAsync();
    }

    private async Task PublishZipCodeMessageAsync(string zipCode, CancellationToken cancellationToken)
    {
        var zipCodeMessage = new ZipCodeMessage { ZipCode = zipCode };

        await _publishEndpoint.Publish(zipCodeMessage, cancellationToken);
    }
}
