using Customers.Management.Application.Interfaces;
using Customers.Management.Application.Services;
using Customers.Management.Application.Validators;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Customers.Management.Application.Tests")]
namespace Customers.Management.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddScoped<ICustomerService, CustomerService>();

        servicesCollection.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var configuration = context.GetRequiredService<IConfiguration>();

                cfg.Host(configuration["RabbitMq:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"]!);
                    h.Password(configuration["RabbitMq:Password"]!);
                });
            });
        });

        servicesCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        servicesCollection.AddValidatorsFromAssemblyContaining<CustomerInsertValidator>();
        servicesCollection.AddValidatorsFromAssemblyContaining<CustomerUpdateValidator>();

        return servicesCollection;
    }
}
