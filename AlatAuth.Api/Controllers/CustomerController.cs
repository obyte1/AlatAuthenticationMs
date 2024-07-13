using AlatAuth.Business.Service.Interface;
using AlatAuth.Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlatAuth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(ApiResponse<>), 400)]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid customer data.");
            }
            var result = await _customerService.CreateCustomer(request);
            if (result is not null && result is string)
            {
                return BadRequest(result); // Return error message
            }
            return Ok(result); // Return success response
        }

        [HttpGet("all-customers")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(ApiResponse<>), 400)]
        public async Task<IActionResult> GetCustomers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _customerService.GetCustomers(pageNumber, pageSize);
            return Ok(result); // Return the list of customers
        }
    }
}
