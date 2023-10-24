using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PaymentService.Database.Context.Extensions;

public static class MigrationExtension
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<PaymentServiceContext>();
        
        context.Database.Migrate();

        return host;
    }
}