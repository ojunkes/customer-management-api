using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Domain.Entities;

namespace Customers.Management.Application.Services;

public interface ICustomerService
{
    Task<CustomerResponse> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(CancellationToken cancellationToken);

    Task<CustomerResponse> InsertCustomerAsync(CustomerRequest request, CancellationToken cancellationToken);

    Task<CustomerResponse> UpdateCustomerAsync(CustomerRequest request, CancellationToken cancellationToken);

    Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
}
