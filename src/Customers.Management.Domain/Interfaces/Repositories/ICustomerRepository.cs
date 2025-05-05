using Customers.Management.Domain.Entities;

namespace Customers.Management.Domain.Interfaces.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<Customer?> GetByTaxIdAsync(string taxId, CancellationToken cancellationToken);
}
