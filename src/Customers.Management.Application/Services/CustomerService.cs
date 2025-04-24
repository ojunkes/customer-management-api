using AutoMapper;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Application.Shared;
using Customers.Management.Domain.Entities;
using Customers.Management.Infra.Repositories;

namespace Customers.Management.Application.Services;

internal class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<CustomerResponse>>(customers);
    }

    public async Task<CustomerResponse> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerResponse> InsertCustomerAsync(CustomerInsertRequest request, CancellationToken cancellationToken)
    {
        if (request?.Id != null)
            throw new ValidationException("Id informado no corpo da requisição.");


        var customerExist = await _customerRepository.GetByCpfAsync(request!.Cpf, cancellationToken);
        if (customerExist != null)
            throw new ValidationException("CPF já consta na base de dados.");

        var customer = _mapper.Map<Customer>(request);

        await _customerRepository.InsertAsync(customer, cancellationToken);
        await _customerRepository.CommitAsync();

        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerResponse> UpdateCustomerAsync(CustomerUpdateRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
            throw new ValidationException("Customer não encontrado.");

        _mapper.Map(request, customer);

        await _customerRepository.Update(customer, cancellationToken);
        await _customerRepository.CommitAsync();

        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);
        if (customer == null)
            throw new ValidationException("Customer não encontrado.");

        await _customerRepository.DeleteAsync(customer, cancellationToken);
        await _customerRepository.CommitAsync();
    }
}
