using System.Collections.Concurrent;
using GatewayService.CircuitBreaker.Configurations;
using Microsoft.Extensions.Options;

namespace GatewayService.CircuitBreaker;

public class CircuitBreakersCache
{
    private readonly ConcurrentDictionary<string, CircuitBreaker> _servicesCircuitBreakers;
    private readonly CircuitBreakerConfiguration _configuration;

    public CircuitBreakersCache(IOptions<CircuitBreakerConfiguration> configuration)
    {
        _configuration = configuration.Value;
        
        _servicesCircuitBreakers = new ConcurrentDictionary<string, CircuitBreaker>();
    }

    public CircuitBreaker GetCircuitBreakerForService(string serviceName)
    {
        if (_servicesCircuitBreakers.TryGetValue(serviceName, out var service))
        {
            return service;
        }

        var circuitBreaker = new CircuitBreaker(_configuration);

        _servicesCircuitBreakers[serviceName] = circuitBreaker;

        return circuitBreaker;
    }
}