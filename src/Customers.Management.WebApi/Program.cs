using Customers.Management.Application.DependencyInjection;
using Customers.Management.Infra.DependencyInjection;

namespace Customers.Management.WebApi;

public class Program
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

        var app = builder.Build();

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