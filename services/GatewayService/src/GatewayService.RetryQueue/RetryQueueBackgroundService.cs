using Microsoft.Extensions.Hosting;

namespace GatewayService.RetryQueue;

public class RetryQueueBackgroundService : BackgroundService
{
    private readonly IRequestsQueue _requestsQueue;

    public RetryQueueBackgroundService(IRequestsQueue requestsQueue)
    {
        _requestsQueue = requestsQueue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            var requestTasks = _requestsQueue.GetRequests().Select(async(a) =>
            {
                try
                {
                    await Task.Run(a, stoppingToken);
                    _requestsQueue.RemoveRequest(a);
                }
                catch (Exception)
                {
                    // pass
                }
            });

            await Task.WhenAll(requestTasks);

            await Task.Delay(10_000, stoppingToken);
        }
    }
}