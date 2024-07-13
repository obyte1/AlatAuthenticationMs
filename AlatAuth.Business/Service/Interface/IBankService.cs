using AlatAuth.Data.Dto;

namespace AlatAuth.Business.Service.Interface
{
    public interface IBankService
    {
        Task<ApiResponse> GetBanks();
    }
}
