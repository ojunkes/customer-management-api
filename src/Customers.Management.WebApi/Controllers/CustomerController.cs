using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Management.WebApi.Controllers
{
    [ApiController]
    [Route(RouteTemplate)]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private const string RouteTemplate = "api/v1/customers";

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            return Ok(await _customerService.GetAllCustomersAsync(cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCustomerAsync(Guid id, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id, cancellationToken);

            return customer == null ? NoContent() : Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCustomerAsync(CustomerInsertRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.InsertCustomerAsync(request, cancellationToken);

            return customer == null ? BadRequest() : Ok(customer);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomerAsync(Guid id, CustomerUpdateRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest("Id da url não é o mesmo do Id do corpo da requisição.");
            }

            var customer = await _customerService.UpdateCustomerAsync(request, cancellationToken);

            return customer == null ? BadRequest() : Ok(customer);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
        {
            await _customerService.DeleteCustomerAsync(id, cancellationToken);

            return Ok();
        }
    }
}
