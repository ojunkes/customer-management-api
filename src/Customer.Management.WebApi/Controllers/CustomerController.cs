using Microsoft.AspNetCore.Mvc;

namespace Customer.Management.WebApi.Controllers
{
    [ApiController]
    [Route(RouteTemplate)]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private const string RouteTemplate = "api/v1/customers";

        [HttpGet]
        public Task<bool> GetAll()
        {
            return Task.FromResult(true);
        }

    }
}
