using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GatewayService.CircuitBreaker;

public class CircuitBreakerInterceptor : Interceptor
{
    private readonly CircuitBreakersCache _circuitBreakersCache;

    public CircuitBreakerInterceptor(CircuitBreakersCache circuitBreakersCache)
    {
        _circuitBreakersCache = circuitBreakersCache;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var circuitBreaker = _circuitBreakersCache.GetCircuitBreakerForService(context.Method.ServiceName);

        if (circuitBreaker.Status is CircuitBreakerStatus.Open)
            throw new RpcException(new Status(StatusCode.Unavailable, "Circuit breaker timeout does not expired"));
        
        var call = continuation(request, context);
        
        return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync, circuitBreaker),
            call.ResponseHeadersAsync,
            call.GetStatus,
            call.GetTrailers,
            call.Dispose);
    }
    
    private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> inner, CircuitBreaker circuitBreaker)
    {
        try
        {
            var response = await inner;
            
            circuitBreaker.AddRequest(ServiceRequestStatus.Success, DateTimeOffset.Now);

            return response;
        }
        catch (RpcException e) when(e.StatusCode is StatusCode.Unavailable)
        {
            circuitBreaker.AddRequest(ServiceRequestStatus.Failure, DateTimeOffset.Now);
            throw;
        }
    }
}