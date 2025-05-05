using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Interfaces.Repositories;
using Customers.Management.Infra.Context;
using Customers.Management.Infra.Context.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Customers.Management.Infra.Repositories;

internal class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Customer?> GetByTaxIdAsync(string taxId, CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<Customer>()
            .FirstOrDefaultAsync(c => c.TaxId == taxId, cancellationToken);
    }
}
