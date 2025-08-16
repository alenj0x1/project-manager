using ProjectManager.Application.Helpers;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Utils;

namespace ProjectManager.WebApi.Middlewares;

public class ErrorHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException e)
        {
            context.Response.StatusCode = ResponseHttpCodes.BadRequest;

            await context.Response.WriteAsJsonAsync(ResponseHelper.Create(ResponseConsts.MiddlewareErrorBadRequest, message: e.Message));
        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = ResponseHttpCodes.NotFound;

            await context.Response.WriteAsJsonAsync(ResponseHelper.Create(ResponseConsts.MiddlewareErrorNotFound, message: e.Message));
        }
        catch (UnauthorizedException e)
        {
            context.Response.StatusCode = ResponseHttpCodes.Unauthorized;

            await context.Response.WriteAsJsonAsync(ResponseHelper.Create(ResponseConsts.MiddlewareErrorUnauthorized, message: e.Message));
        }
        catch (Exception e)
        {
            context.Response.StatusCode = ResponseHttpCodes.InternalServerError;

            await context.Response.WriteAsJsonAsync(ResponseHelper.Create(ResponseConsts.MiddlewareErrorInternalServerError, message: e.Message));
        }
    }
}