using Customers.Management.Domain.Entities;
using Customers.Management.Infra.Context.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Customers.Management.Infra.Context;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext, IContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public async Task<bool> CommitAsync()
    {
        foreach (var entry in ChangeTracker.Entries()
                                .Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property("CreatedAt").CurrentValue = DateTimeOffset.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Property("ModifiedAt").CurrentValue = DateTimeOffset.UtcNow;
                    entry.Property("CreatedAt").IsModified = false;
                    break;
            }
        }

        var result = await base.SaveChangesAsync();
        return result > 0;
    }

    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    {
        return Set<TEntity>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
