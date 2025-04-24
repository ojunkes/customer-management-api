using Customers.Management.Application.Responses;
using Customers.Management.Application.Shared;
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
            case DomainException:
                _logger.LogWarning(exception, "Erro de validação dominio: {message}", exception.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            default:
                _logger.LogError(exception, "Erro não esperado: {message}", exception.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        var responseJson = JsonSerializer.Serialize(
            BaseApiResponse<object>.Fail(exception.Message)
        );
        return context.Response.WriteAsync(responseJson);
    }
}
