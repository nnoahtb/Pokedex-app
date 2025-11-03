namespace Backend.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate _next, ILogger<ExceptionHandlingMiddleware> _logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);

        try
        {
            await _next(context);
            _logger.LogInformation("Finished handling request. Status Code: {StatusCode}", context.Response.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred {Method} {Path}", context.Request.Method, context.Request.Path);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                message = "An unexpected error occurred. Please try again later."
            };

            await context.Response.WriteAsJsonAsync(response);

            _logger.LogInformation("Returned 500 response for: {Method} {Path}", context.Request.Method, context.Request.Path);
        }
    }
}
