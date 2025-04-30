using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Application.Services;
using Customers.Management.Application.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Customer.Management.WebApi.Controllers;

[ApiController]
[Route(RouteTemplate)]
[Produces("application/json")]
[ExcludeFromCodeCoverage]
public class CustomerController : ControllerBase
{
    private const string RouteTemplate = "api/v1/customers";

    private readonly ICustomerService _customerService;
    private readonly CustomerInsertValidator _insertValidator;
    private readonly CustomerUpdateValidator _updateValidator;

    public CustomerController(
        ICustomerService customerService,
        CustomerInsertValidator insertValidator,
        CustomerUpdateValidator updateValidator)
    {
        _customerService = customerService;
        _insertValidator = insertValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(BaseApiResponse<IEnumerable<CustomerResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var customers = await _customerService.GetAllCustomersAsync(cancellationToken);

        return customers == null ? NoContent() : Ok(customers);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BaseApiResponse<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetCustomerAsync(id, cancellationToken);

        return customer == null ? NoContent() : Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseApiResponse<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> InsertCustomerAsync(CustomerRequest request, CancellationToken cancellationToken)
    {
        var result = await _insertValidator.ValidateAsync(request);
        if (!result.IsValid)
            return BadRequest(BaseApiResponse<object>.Fail(
                result.Errors.Select(e => e.ErrorMessage)
            ));

        var customer = await _customerService.InsertCustomerAsync(request, cancellationToken);

        return customer == null ? BadRequest() : Ok(customer);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BaseApiResponse<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomerAsync(Guid id, CustomerRequest request, CancellationToken cancellationToken)
    {
        var result = await _updateValidator.ValidateAsync(request);
        if (!result.IsValid)
            return BadRequest(BaseApiResponse<object>.Fail(
                result.Errors.Select(e => e.ErrorMessage)
            ));

        if (id != request.Id)
            return BadRequest(BaseApiResponse<object>.Fail("Id da url não é o mesmo do Id do corpo da requisição."));

        var customer = await _customerService.UpdateCustomerAsync(request, cancellationToken);

        return customer == null ? BadRequest() : Ok(customer);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(BaseApiResponse<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        await _customerService.DeleteCustomerAsync(id, cancellationToken);

        return Ok();
    }
}
