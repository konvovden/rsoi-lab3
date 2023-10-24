using Grpc.Core;
using Grpc.Core.Interceptors;
using RentalService.Core.Exceptions;

namespace RentalService.Server.Interceptors;

public class ExceptionsHandlingInterceptor : Interceptor
{
    private readonly ILogger<ExceptionsHandlingInterceptor> _logger;

    public ExceptionsHandlingInterceptor(ILogger<ExceptionsHandlingInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception e)
        {
            throw e switch
            {
                ForbiddenException => new RpcException(new Status(StatusCode.PermissionDenied, "Недостаточно прав.")),
                RentalNotFoundException => new RpcException(new Status(StatusCode.NotFound, "Аренда не найдена.")),
                RentalNotInProgressException => new RpcException(new Status(StatusCode.FailedPrecondition, "Аренда не в процессе.")),
                _ => new RpcException(new Status(StatusCode.Internal, "Внутренняя ошибка сервера."))
            };
        }
    }
}