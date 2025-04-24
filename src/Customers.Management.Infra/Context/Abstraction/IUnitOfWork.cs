namespace Customers.Management.Infra.Context.Abstraction;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
