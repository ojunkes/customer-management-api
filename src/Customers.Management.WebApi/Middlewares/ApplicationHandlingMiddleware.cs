using Customers.Management.Application.Shared;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Customers.Management.WebApi.Middlewares;

public class ApplicationHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApplicationHandlingMiddleware> _logger;

    public ApplicationHandlingMiddleware(RequestDelegate next, ILogger<ApplicationHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        string title = string.Empty;

        switch (exception)
        {
            case ValidationException:
                title = "Erro de validação";
                _logger.LogWarning(exception, "{title}: {message}", title, exception.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            default:
                title = "Erro não esperado";
                _logger.LogError(exception, "{title}: {message}", title, exception.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }
        var responseJson = JsonSerializer.Serialize(new
        {
            title = title,
            detail = exception.Message
        });
        return context.Response.WriteAsync(responseJson);
    }
}
