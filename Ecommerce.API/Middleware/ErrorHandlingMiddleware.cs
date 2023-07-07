using System.Text.Json;

namespace Ecommerce.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            // Handle the exception and generate an appropriate response
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Set the response status code
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        // Customize the error response
        var response = new
        {
            error = "An unexpected error occurred."
        };

        // Serialize the response as JSON
        var json = JsonSerializer.Serialize(response);

        // Write the JSON response to the HTTP response
        await context.Response.WriteAsync(json);
    }
}