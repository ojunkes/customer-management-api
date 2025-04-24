using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;

namespace Customers.Management.Application.Services;

public interface ICustomerService
{
    Task<BaseApiResponse<CustomerResponse>> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<BaseApiResponse<IEnumerable<CustomerResponse>>> GetAllCustomersAsync(CancellationToken cancellationToken);

    Task<BaseApiResponse<CustomerResponse>> InsertCustomerAsync(CustomerRequest request, CancellationToken cancellationToken);

    Task<BaseApiResponse<CustomerResponse>> UpdateCustomerAsync(CustomerRequest request, CancellationToken cancellationToken);

    Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
}
