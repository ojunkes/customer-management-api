using Customers.Management.Domain.Entities;

namespace Customers.Management.Domain.Interfaces.Repositories;

public interface IAddressRepository
{
    Task<Address?> GetByZipCodeAsync(string zipCode, CancellationToken cancellationToken);

    Task InsertAsync(Address address, CancellationToken cancellationToken);

    Task Update(Address address, CancellationToken cancellationToken);

    Task CommitAsync();
}
