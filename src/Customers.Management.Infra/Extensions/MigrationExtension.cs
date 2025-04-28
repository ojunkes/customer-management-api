using Customers.Management.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Customers.Management.Infra.Extensions;

[ExcludeFromCodeCoverage]
public static class MigrationExtension
{
    public static void UseDatabaseMigration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var servcices = serviceScope.ServiceProvider;

        var context = servcices.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();
    }
}
