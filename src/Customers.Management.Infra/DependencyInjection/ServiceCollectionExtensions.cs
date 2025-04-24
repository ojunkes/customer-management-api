using Customers.Management.Infra.Context;
using Customers.Management.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Customers.Management.Infra.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContextSqlServer(this IServiceCollection serviceCollection, string sqlServerConnectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(sqlServerConnectionString));

        return serviceCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();

        return serviceCollection;
    }
}
