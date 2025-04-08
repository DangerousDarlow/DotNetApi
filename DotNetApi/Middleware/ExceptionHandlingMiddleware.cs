using Npgsql;

namespace DotNetApi.Middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NpgsqlException ex)
        {
            logger.LogError(ex, "Unhandled database exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}