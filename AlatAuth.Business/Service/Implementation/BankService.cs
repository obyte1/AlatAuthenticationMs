using AlatAuth.Business.Adapter;
using AlatAuth.Business.Service.Interface;
using AlatAuth.Common.Enum;
using AlatAuth.Data.Dto;
using AlatAuth.Data.Dto.Banks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AlatAuth.Business.Service.Implementation
{
    public class BankService : GenericAdapter, IBankService
    {
        private readonly IConfiguration _configuration;
        public BankService(IHttpClientFactory httpClient, IHttpContextAccessor httpContext, IConfiguration configuration)
            : base(httpClient, httpContext)
        {
            _configuration = configuration;
        }

        public async Task<ApiResponse> GetBanks()
        {
            var requestModel = new APIRequestModel
            {
                ApiType = ApiType.Get,
                Url = _configuration["Alat:AlatBaseUrl"] + "GetAllBanks"
            };
            var banks = await this.SendAsync<GetBankResponseDto>(requestModel);
          return ResponseHandler.SuccessResponse("Bank list fetched successfully!",banks.result);
        }
    }
}
