using Customers.Management.Consumer.Adapters;
using Customers.Management.Consumer.Consumers;
using Customers.Management.Domain.Interfaces;
using Customers.Management.Infra.DependencyInjection;
using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace Customers.Management.Consumer;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddLogging();

        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddMessagingConsumer<ZipCodeMessageConsumer>(builder.Configuration);

        builder.Services.AddScoped<IViaCepAdapter, ViaCepAdapter>();
        builder.Services.AddHttpClient<IViaCepAdapter, ViaCepAdapter>();

        var host = builder.Build();
        host.Run();
    }
}
