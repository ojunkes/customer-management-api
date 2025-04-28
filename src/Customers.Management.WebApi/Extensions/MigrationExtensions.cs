using Customers.Management.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Customers.Management.WebApi.Extensions;

public static class MigrationExtensions
{
    public static void UseDatabaseMigration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var servcices = serviceScope.ServiceProvider;

        var context = servcices.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();
    }
}
