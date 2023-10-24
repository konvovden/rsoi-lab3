using CarsService.Core.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace CarsService.Server.Interceptors;

public class ExceptionsHandlingInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, 
        ServerCallContext context,
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
                CarNotFoundException => new RpcException(new Status(StatusCode.NotFound, "Машина не найдена.")),
                CarAlreadyReservedException => new RpcException(new Status(StatusCode.FailedPrecondition, "Машина уже зарезервирована.")),
                _ => new RpcException(new Status(StatusCode.Internal, "Внутренняя ошибка сервера."))
            };
        }
    }
}