using Customers.Management.Domain.Entities;
using Customers.Management.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Customers.Management.Infra.Repositories;

internal class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>?> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<Customer>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<Customer>()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task InsertAsync(Customer customer, CancellationToken cancellationToken)
    {
        await _context.GetDbSet<Customer>()
            .AddAsync(customer, cancellationToken);
    }

    public Task Update(Customer customer, CancellationToken cancellationToken)
    {
        _context.Entry(customer).State = EntityState.Modified;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = _context.GetDbSet<Customer>().Find(id);
        if (customer != null)
        {
            _context.GetDbSet<Customer>().Remove(customer);
        }

        return Task.CompletedTask;
    }

    public async Task CommitAsync()
    {
        await _context.CommitAsync();
    }
}
