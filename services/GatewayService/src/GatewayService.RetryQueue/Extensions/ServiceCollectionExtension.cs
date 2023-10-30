using GatewayService.RetryQueue.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GatewayService.RetryQueue.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRetryQueue(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IRequestsQueue, RequestsQueue>();

        serviceCollection.AddHostedService<RetryQueueBackgroundService>();
        serviceCollection.Configure<RetryQueueConfiguration>(
            configuration.GetRequiredSection(nameof(RetryQueueConfiguration)));

        return serviceCollection;
    }
}