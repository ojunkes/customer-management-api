using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;

namespace Customers.Management.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerResponse> GetCustomerAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(CancellationToken cancellationToken);

    Task<CustomerResponse> InsertCustomerAsync(CustomerRequest request, CancellationToken cancellationToken);

    Task<CustomerResponse> UpdateCustomerAsync(CustomerRequest request, CancellationToken cancellationToken);

    Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
}
