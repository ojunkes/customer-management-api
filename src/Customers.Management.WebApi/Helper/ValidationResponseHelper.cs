using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Management.WebApi.Helper;

public static class ValidationResponseHelper
{
    public static IActionResult ToBadRequest(ValidationResult result)
    {
        var response = new
        {
            title = "Erro validação campos de entrada",
            detail = result.Errors.Select(e => e.ErrorMessage)
        };

        return new BadRequestObjectResult(response);
    }

    public static IActionResult ToBadRequest(string message)
    {
        var response = new
        {
            title = "Erro validação campos de entrada",
            detail = message
        };

        return new BadRequestObjectResult(response);
    }
}
