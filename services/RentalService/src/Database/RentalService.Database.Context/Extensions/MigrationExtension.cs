using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RentalService.Database.Context.Extensions;

public static class MigrationExtension
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<RentalServiceContext>();
        
        context.Database.Migrate();

        return host;
    }
}