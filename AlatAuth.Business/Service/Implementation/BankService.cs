using AlatAuth.Business.Adapter;
using AlatAuth.Business.Service.Interface;
using AlatAuth.Common.Enum;
using AlatAuth.Data.Dto;
using AlatAuth.Data.Dto.Banks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AlatAuth.Business.Service.Implementation
{
    public class BankService : IBankService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericAdapter _genericAdapter;
        public BankService(IGenericAdapter genericAdapter, IConfiguration configuration)
           
        {
            _configuration = configuration;
            _genericAdapter = genericAdapter;
        }

        public async Task<ApiResponse> GetBanks()
        {
            var requestModel = new APIRequestModel
            {
                ApiType = ApiType.Get,
                Url = _configuration["Alat:AlatBaseUrl"] + "GetAllBanks"
            };
            var banks = await _genericAdapter.SendAsync<GetBankResponseDto>(requestModel);
          return ResponseHandler.SuccessResponse("Bank list fetched successfully!",banks.result);
        }
    }
}
