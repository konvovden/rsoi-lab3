using GatewayService.CircuitBreaker.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GatewayService.CircuitBreaker.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCircuitBreaker(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<CircuitBreakerInterceptor>();
        serviceCollection.AddSingleton<CircuitBreakersCache>();

        serviceCollection.Configure<CircuitBreakerConfiguration>(
            configuration.GetRequiredSection(nameof(CircuitBreakerConfiguration)));

        return serviceCollection;
    }
}