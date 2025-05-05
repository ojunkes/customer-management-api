using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Interfaces.Repositories;
using Customers.Management.Infra.Context;
using Customers.Management.Infra.Context.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Customers.Management.Infra.Repositories;

internal class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Address?> GetByZipCodeAsync(string zipCode, CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<Address>()
            .FirstOrDefaultAsync(a => a.ZipCode == zipCode, cancellationToken);
    }
}
