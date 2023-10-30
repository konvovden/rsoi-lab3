using Grpc.Core;

namespace GatewayService.Server.Middlewares;

public class GrpcExceptionsHandleMiddleware
{
    private readonly RequestDelegate _next;

    public GrpcExceptionsHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (RpcException e)
        {
            var statusCode = e.StatusCode switch
            {
                StatusCode.OK => StatusCodes.Status200OK,
                StatusCode.InvalidArgument => StatusCodes.Status400BadRequest,
                StatusCode.NotFound => StatusCodes.Status404NotFound,
                StatusCode.AlreadyExists => StatusCodes.Status409Conflict,
                StatusCode.PermissionDenied => StatusCodes.Status403Forbidden,
                StatusCode.Unauthenticated => StatusCodes.Status401Unauthorized,
                StatusCode.FailedPrecondition => StatusCodes.Status409Conflict,
                StatusCode.Unavailable => StatusCodes.Status503ServiceUnavailable,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(e.Status.Detail);
        }
    }
}