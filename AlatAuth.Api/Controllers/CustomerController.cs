using AlatAuth.Business.Service.Interface;
using AlatAuth.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AlatAuth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest request)
        {
            return Response(await _customerService.CreateCustomer(request));
        }

        [HttpGet("all-customers")]
        [ProducesResponseType(typeof(ApiResponse<List<CustomerResponse>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetCustomers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
           return Response( await _customerService.GetCustomers(pageNumber, pageSize));
        }

        [HttpGet("all-states")]
        [ProducesResponseType(typeof(ApiResponse<List<object>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetStates()
        {
            return Response(await _customerService.GetState());
        }

        [HttpGet("lga-by-stateid")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> Lga(int stateId)
        {
            return Response(await _customerService.GetLgaByStateId(stateId));
        }

        [HttpPost("verify-otp")]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> VarifyOtp(VerifyOtpRequest request)
        {
            return Response(await _customerService.VerifyOtp(request));
        }
    }
}
