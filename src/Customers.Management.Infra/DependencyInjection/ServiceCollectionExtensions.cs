using Customers.Management.Domain.Interfaces.Adapters;
using Customers.Management.Domain.Interfaces.Repositories;
using Customers.Management.Infra.Adapters;
using Customers.Management.Infra.Context;
using Customers.Management.Infra.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Customers.Management.Infra.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!));

        serviceCollection.AddRepositories();

        serviceCollection.AddScoped<IViaCepAdapter, ViaCepAdapter>();
        serviceCollection.AddHttpClient<IViaCepAdapter, ViaCepAdapter>();

        return serviceCollection;
    }

    public static IServiceCollection AddContextSqlServerInMemory(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("DefaultDatabase", new InMemoryDatabaseRoot()));

        serviceCollection.AddRepositories();

        return serviceCollection;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
        return serviceCollection;
    }

    public static IServiceCollection AddMessagingConsumer<T>(this IServiceCollection services, IConfiguration configuration) where T : class, IConsumer
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<T>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"]!);
                    h.Password(configuration["RabbitMq:Password"]!);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static IServiceCollection AddMessagingPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"]!);
                    h.Password(configuration["RabbitMq:Password"]!);
                });
            });
        });

        return services;
    }
}
