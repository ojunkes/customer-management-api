using Customers.Management.Domain.Entities;

namespace Customers.Management.Infra.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>?> GetAllAsync(CancellationToken cancellationToken);

    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task InsertAsync(Customer customer, CancellationToken cancellationToken);

    Task Update(Customer customer, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task CommitAsync();
}
