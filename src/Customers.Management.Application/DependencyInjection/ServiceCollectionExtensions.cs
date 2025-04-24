using Customers.Management.Application.Services;
using Customers.Management.Infra.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Management.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddScoped<ICustomerService, CustomerService>();

        servicesCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return servicesCollection;
    }
}
