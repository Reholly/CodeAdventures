using Server.Exceptions;

namespace Server.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                AuthOperationException => StatusCodes.Status401Unauthorized,
                ServiceOperationNullException => StatusCodes.Status404NotFound,
                ServiceInvalidOperationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}