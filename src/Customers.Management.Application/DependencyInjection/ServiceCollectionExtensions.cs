using Customers.Management.Application.Services;
using Customers.Management.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Customers.Management.Application.Tests")]
namespace Customers.Management.Application.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddScoped<ICustomerService, CustomerService>();

        servicesCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        servicesCollection.AddValidatorsFromAssemblyContaining<CustomerInsertValidator>();
        servicesCollection.AddValidatorsFromAssemblyContaining<CustomerUpdateValidator>();

        return servicesCollection;
    }
}
