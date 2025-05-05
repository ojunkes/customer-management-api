using Customers.Management.Application.Interfaces;
using Customers.Management.Application.Services;
using Customers.Management.Application.Validators;
using Customers.Management.Infra.DependencyInjection;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Customers.Management.Application.Tests")]
namespace Customers.Management.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection, IConfiguration configuration)
    {
        servicesCollection.AddScoped<ICustomerService, CustomerService>();

        servicesCollection.AddInfrastructureServices(configuration);
        servicesCollection.AddMessagingPublisher(configuration);

        servicesCollection.AddValidatorsFromAssembly(typeof(ApplicationValidationAssemblyReference).Assembly);

        return servicesCollection;
    }
}
