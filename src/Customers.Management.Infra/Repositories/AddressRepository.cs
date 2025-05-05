using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Interfaces;
using Customers.Management.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Customers.Management.Infra.Repositories;

internal class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Address?> GetByZipCodeAsync(string zipCode, CancellationToken cancellationToken)
    {
        return await _context.GetDbSet<Address>()
            .FirstOrDefaultAsync(a => a.ZipCode == zipCode, cancellationToken);
    }

    public async Task InsertAsync(Address address, CancellationToken cancellationToken)
    {
        await _context.GetDbSet<Address>()
            .AddAsync(address, cancellationToken);
    }

    public Task Update(Address address, CancellationToken cancellationToken)
    {
        _context.Entry(address).State = EntityState.Modified;

        return Task.CompletedTask;
    }

    public async Task CommitAsync()
    {
        await _context.CommitAsync();
    }
}
