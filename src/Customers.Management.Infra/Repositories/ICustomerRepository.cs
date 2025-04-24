using Customers.Management.Domain.Entities;

namespace Customers.Management.Infra.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>?> GetAllAsync(CancellationToken cancellationToken);

    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);

    Task InsertAsync(Customer customer, CancellationToken cancellationToken);

    Task Update(Customer customer, CancellationToken cancellationToken);

    Task DeleteAsync(Customer customer, CancellationToken cancellationToken);

    Task CommitAsync();
}
