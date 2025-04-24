using Microsoft.EntityFrameworkCore;

namespace Customers.Management.Infra.Context.Abstraction;

public interface IContext : IUnitOfWork
{
    DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
}
