using Customers.Management.Domain.Entities;

namespace Customers.Management.Domain.Interfaces.Repositories;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<Address?> GetByZipCodeAsync(string zipCode, CancellationToken cancellationToken);
}
