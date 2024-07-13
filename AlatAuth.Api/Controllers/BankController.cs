using AlatAuth.Business.Service.Interface;
using AlatAuth.Data.Dto;
using AlatAuth.Data.Dto.Banks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlatAuth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : BaseController
    {
        private readonly IBankService bankService;

        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        [HttpGet("get-bank-list")]
        [ProducesResponseType(typeof(ApiResponse<List<BankDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> GetBankList() 
            => Response(await bankService.GetBanks());
    }
}
