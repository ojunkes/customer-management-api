using Customers.Management.Application.DependencyInjection;
using Customers.Management.Infra.DependencyInjection;
using Customers.Management.WebApi.Middlewares;
using System.Diagnostics.CodeAnalysis;

namespace Customers.Management.WebApi;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

            });
        });

        builder.Services.AddSwaggerGen();

        builder.Services.AddApplicationServices();

        builder.Services.AddDbContextSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
        builder.Services.AddRepositories();

        var app = builder.Build();

        app.UseMiddleware<ApplicationHandlingMiddleware>();

        app.UseCors();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}