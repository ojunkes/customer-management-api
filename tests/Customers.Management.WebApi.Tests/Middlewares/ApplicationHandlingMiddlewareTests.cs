using Customers.Management.Application.Responses;
using Customers.Management.Application.Commons;
using Customers.Management.WebApi.Middlewares;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;
using Xunit;

namespace Customers.Management.WebApi.Tests.Middlewares;

public class ApplicationHandlingMiddlewareTests
{
    [Fact]
    public async Task ApplicationHandlingMiddleware_ShouldReturnStatus400_WhenDomainExceptionIsThrown()
    {
        var loggerMock = new Mock<ILogger<ApplicationHandlingMiddleware>>();
        var context = new DefaultHttpContext();
        var middleware = new ApplicationHandlingMiddleware(
            _ => throw new DomainException("Erro de domínio"), loggerMock.Object);

        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await middleware.InvokeAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        responseStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(responseStream).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<BaseApiResponse<object>>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        response.Should().NotBeNull();
        response!.Success.Should().BeFalse();
        response.Errors.Should().Contain("Erro de domínio");
    }

    [Fact]
    public async Task ApplicationHandlingMiddleware_ShouldReturnStatus500_WhenUnhandledExceptions()
    {
        var loggerMock = new Mock<ILogger<ApplicationHandlingMiddleware>>();
        var context = new DefaultHttpContext();
        var middleware = new ApplicationHandlingMiddleware(
            _ => throw new Exception("Erro genérico"), loggerMock.Object);

        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        await middleware.InvokeAsync(context);

        context.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);

        responseStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(responseStream).ReadToEndAsync();
        var response = JsonSerializer.Deserialize<BaseApiResponse<object>>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        response.Should().NotBeNull();
        response!.Success.Should().BeFalse();
        response.Errors.Should().Contain("Erro genérico");
    }
}
