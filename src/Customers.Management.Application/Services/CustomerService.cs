using AutoMapper;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
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
        var customerExist = await _customerRepository.GetByCpfAsync(request.Cpf, cancellationToken);
        //TODO: Validar se já existe CPF - criar tratamento de erros
        //TODO: Validar se id preenchido

        var customer = _mapper.Map<Customer>(request);
        
        await _customerRepository.InsertAsync(customer, cancellationToken);
        await _customerRepository.CommitAsync();

        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerResponse> UpdateCustomerAsync(CustomerUpdateRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        //TODO: Validar se id request diff id entidade - criar tratamento de erros

        _mapper.Map(request, customer);

        await _customerRepository.Update(customer, cancellationToken);
        await _customerRepository.CommitAsync();

        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customerExist = await _customerRepository.GetByIdAsync(id, cancellationToken);
        //TODO: Validar se id existe - criar tratamento de erros

        await _customerRepository.DeleteAsync(customerExist, cancellationToken);
        await _customerRepository.CommitAsync();
    }
}
