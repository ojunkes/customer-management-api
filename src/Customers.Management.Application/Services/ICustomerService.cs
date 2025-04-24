using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Domain.Entities;

namespace Customers.Management.Application.Services;

public interface ICustomerService
{
    Task<CustomerResponse> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(CancellationToken cancellationToken);

    Task<CustomerResponse> InsertCustomerAsync(CustomerInsertRequest request, CancellationToken cancellationToken);

    Task<CustomerResponse> UpdateCustomerAsync(CustomerUpdateRequest request, CancellationToken cancellationToken);

    Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
}
