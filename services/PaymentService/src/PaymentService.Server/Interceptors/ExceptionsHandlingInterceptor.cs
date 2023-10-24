using Grpc.Core;
using Grpc.Core.Interceptors;
using PaymentService.Core.Exceptions;

namespace PaymentService.Server.Interceptors;

public class ExceptionsHandlingInterceptor : Interceptor
{
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
                PaymentNotFoundException => new RpcException(new Status(StatusCode.NotFound, "Платёж не найден.")),
                PaymentNotPaidException => new RpcException(new Status(StatusCode.FailedPrecondition, "Платёж не завершён.")),
                _ => new RpcException(new Status(StatusCode.Internal, "Внутренняя ошибка сервера."))
            };
        }
    }
}