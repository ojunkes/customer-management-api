using Customers.Management.Application.DependencyInjection;
using Customers.Management.Infra.DependencyInjection;
using Customers.Management.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc;

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
        builder.Services.AddRepositories();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var detail = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                    );

                var customResponse = new
                {
                    title = "Erro de validação nos campos enviados.",
                    detail
                };

                return new BadRequestObjectResult(customResponse);
            };
        });

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