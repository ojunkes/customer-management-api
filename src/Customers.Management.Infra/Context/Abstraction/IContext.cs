using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Customers.Management.Infra.Context.Abstraction;

public interface IContext : IUnitOfWork
{
    DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
}
