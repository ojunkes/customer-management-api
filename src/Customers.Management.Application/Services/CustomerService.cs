using AutoMapper;
using Customers.Management.Application.Commons;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Messages;
using Customers.Management.Infra.Repositories;
using MassTransit;

namespace Customers.Management.Application.Services;

internal class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public CustomerService(
        ICustomerRepository customerRepository,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public async Task<BaseApiResponse<IEnumerable<CustomerResponse>>> GetAllCustomersAsync(CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        var customersResponse = _mapper.Map<IEnumerable<CustomerResponse>>(customers);

        return BaseApiResponse<IEnumerable<CustomerResponse>>.Ok(customersResponse);
    }

    public async Task<BaseApiResponse<CustomerResponse>> GetCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
        if (customer == null)
            throw new DomainException("Cliente não encontrado.");

        var customersResponse = _mapper.Map<CustomerResponse>(customer);

        return BaseApiResponse<CustomerResponse>.Ok(customersResponse);
    }

    public async Task<BaseApiResponse<CustomerResponse>> InsertCustomerAsync(CustomerRequest request, CancellationToken cancellationToken)
    {
        if (request?.Id != Guid.Empty)
            throw new DomainException("Id informado no corpo da requisição.");

        var customerExist = await _customerRepository.GetByTaxIdAsync(request.TaxId!, cancellationToken);
        if (customerExist != null)
            throw new DomainException("CPF já consta na base de dados.");

        var customer = _mapper.Map<Customer>(request);

        await _customerRepository.InsertAsync(customer, cancellationToken);
        await _customerRepository.CommitAsync();

        var zipCodeMessage = new ZipCodeMessage { ZipCode = request.ZipCode! };

        await _publishEndpoint.Publish(zipCodeMessage, cancellationToken);

        var customersResponse = _mapper.Map<CustomerResponse>(customer);

        return BaseApiResponse<CustomerResponse>.Ok(customersResponse);
    }

    public async Task<BaseApiResponse<CustomerResponse>> UpdateCustomerAsync(CustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
            throw new DomainException("Cliente não encontrado.");

        _mapper.Map(request, customer);

        await _customerRepository.Update(customer, cancellationToken);
        await _customerRepository.CommitAsync();

        var customersResponse = _mapper.Map<CustomerResponse>(customer);

        return BaseApiResponse<CustomerResponse>.Ok(customersResponse);
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
        if (customer == null)
            throw new DomainException("Cliente não encontrado.");

        await _customerRepository.DeleteAsync(customer, cancellationToken);
        await _customerRepository.CommitAsync();
    }
}
