using Customers.Management.Application.Services;
using Customers.Management.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Management.Application.DependencyInjection;

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
