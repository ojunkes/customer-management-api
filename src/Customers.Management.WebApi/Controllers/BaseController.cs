using Customers.Management.Application.Responses;
using Customers.Management.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Management.WebApi.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(typeof(BaseApiResponse<IEnumerable<CustomerResponse>>), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(BaseApiResponse<CustomerResponse>), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(BaseApiResponse<object>), StatusCodes.Status400BadRequest)]
public abstract class BaseController : ControllerBase
{
    protected ActionResult Success(object? data = null) =>
        Ok( new BaseApiResponse<object> { Success = true, Data = data });

    protected ActionResult Fail(string? error = null) =>
        BadRequest(new BaseApiResponse<object> { Success = false, Errors = new List<string> { error } });

    protected ActionResult Fail(IEnumerable<string> errors) =>
        BadRequest(new BaseApiResponse<object> { Success = false, Errors = errors });
}
