using Customers.Management.Infra.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Customers.Management.Infra.Tests.Fixtures;

public sealed class FixtureServiceProvider
{
    private readonly ServiceProvider _serviceProvider;

    public FixtureServiceProvider()
    {
        var serviceCollection = new ServiceCollection()
            .AddContextSqlServerInMemory()
            .AddRepositories();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public TRepository GetService<TRepository>() => _serviceProvider.GetService<TRepository>()!;
}
