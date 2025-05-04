using Customers.Management.Consumer.Adapters;
using Customers.Management.Consumer.Consumers;
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

        builder.Services.AddDbContextSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
        builder.Services.AddRepositories();

        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<ZipCodeMessageConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var configuration = context.GetRequiredService<IConfiguration>();

                cfg.Host(configuration["RabbitMq:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"]!);
                    h.Password(configuration["RabbitMq:Password"]!);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        builder.Services.AddScoped<IViaCepAdapter, ViaCepAdapter>();
        builder.Services.AddHttpClient<IViaCepAdapter, ViaCepAdapter>();

        var host = builder.Build();
        host.Run();
    }
}
