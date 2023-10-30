namespace GatewayService.CircuitBreaker.Configurations;

public class CircuitBreakerConfiguration
{
    public int FailedRequestsLimit { get; set; }
    public TimeSpan BreakDuration { get; set; }
}