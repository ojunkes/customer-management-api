using Customers.Management.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Customers.Management.Infra.Context.Abstraction;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<TEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<TEntity>()
            .FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.GetDbSet<TEntity>().AddAsync(entity, cancellationToken);
    }

    public Task Update(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.GetDbSet<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task CommitAsync()
    {
        await _context.CommitAsync();
    }
}

