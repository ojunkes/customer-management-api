using Customers.Management.Infra.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Management.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection servicesCollection)
    {
        return servicesCollection;
    }
}
