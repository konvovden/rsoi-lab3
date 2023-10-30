using GatewayService.CircuitBreaker.Configurations;

namespace GatewayService.CircuitBreaker;

public class CircuitBreaker
{
    private readonly CircuitBreakerConfiguration _configuration;
    
    private int _failedRequestsCount;
    private DateTimeOffset? _lastFailedRequestTimestamp;

    public CircuitBreaker(CircuitBreakerConfiguration configuration)
    {
        _configuration = configuration;

        _failedRequestsCount = 0;
        _lastFailedRequestTimestamp = null;
    }

    public CircuitBreakerStatus Status
    {
        get
        {
            if (_failedRequestsCount > _configuration.FailedRequestsLimit)
            {
                return DateTimeOffset.Now - _lastFailedRequestTimestamp > _configuration.BreakDuration
                    ? CircuitBreakerStatus.HalfOpen
                    : CircuitBreakerStatus.Open;
            }

            return CircuitBreakerStatus.Closed;
        }
    }

    public void AddRequest(ServiceRequestStatus requestStatus, DateTimeOffset timestamp)
    {
        if (requestStatus is ServiceRequestStatus.Success)
        {
            _failedRequestsCount = 0;
            _lastFailedRequestTimestamp = null;
        }
        else
        {
            _failedRequestsCount++;
            _lastFailedRequestTimestamp = timestamp;
        }
    }
}

public enum CircuitBreakerStatus
{
    Closed = 0,
    HalfOpen = 1,
    Open = 2
}

public enum ServiceRequestStatus
{
    Success = 0,
    Failure = 1
}