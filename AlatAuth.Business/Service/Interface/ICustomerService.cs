using AlatAuth.Data.Dto;

namespace AlatAuth.Business.Service.Interface
{
    public interface ICustomerService
    {
        Task<ApiResponse> CreateCustomer(CustomerRequest request);
        Task<ApiResponse> GetCustomers(int pageNumber, int pageSize);
        Task<ApiResponse> GetState();
        Task<ApiResponse> GetLgaByStateId(int stateId);
        Task<ApiResponse> VerifyOtp(VerifyOtpRequest request);
    }
}
