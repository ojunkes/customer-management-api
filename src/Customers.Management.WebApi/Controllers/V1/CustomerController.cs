using Asp.Versioning;
using Customers.Management.Application.Interfaces;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Customers.Management.WebApi.Controllers.v1;

[ExcludeFromCodeCoverage]
[Route("api/v{version:apiVersion}/customers")]
[ApiVersion("1.0")]
public class CustomerController : BaseController
{
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
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var customers = await _customerService.GetAllCustomersAsync(cancellationToken);

        return Success(customers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetCustomerAsync(id, cancellationToken);

        return Success(customer);
    }

    [HttpPost]
    public async Task<IActionResult> InsertCustomerAsync(CustomerRequest request, CancellationToken cancellationToken)
    {
        var result = await _insertValidator.ValidateAsync(request);
        if (!result.IsValid)
            return Fail(result.Errors.Select(e => e.ErrorMessage));

        var customer = await _customerService.InsertCustomerAsync(request, cancellationToken);

        return customer == null ? Fail() : Success(customer);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomerAsync(Guid id, CustomerRequest request, CancellationToken cancellationToken)
    {
        var result = await _updateValidator.ValidateAsync(request);
        if (!result.IsValid)
            return Fail(result.Errors.Select(e => e.ErrorMessage));

        if (id != request.Id)
            return Fail("Id da url não é o mesmo do Id do corpo da requisição.");

        var customer = await _customerService.UpdateCustomerAsync(request, cancellationToken);

        return customer == null ? Fail() : Success(customer);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        await _customerService.DeleteCustomerAsync(id, cancellationToken);

        return Success();
    }
}
