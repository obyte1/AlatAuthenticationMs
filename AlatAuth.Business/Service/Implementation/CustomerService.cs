using AlatAuth.Business.Service.Interface;
using AlatAuth.Common.Enum;
using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.Dto;
using AlatAuth.Data.Entity;
using Azure;
using Mapster;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AlatAuth.Business.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> CreateCustomer(CustomerRequest request)
        {
            var existingCustomer = await _unitOfWork.CustomerRepo.GetFirstOrDefaultAsync(filter: x => x.PhoneNumber == request.PhoneNumber);
            if (existingCustomer != null)
            {
                return ResponseHandler.FailureResponse("400", "Phone number already Exist");
            }
            var emailExist = await _unitOfWork.CustomerRepo.GetFirstOrDefaultAsync(filter: x => x.Email == request.Email);
            if (emailExist != null)
            {
                return ResponseHandler.FailureResponse("400", "Email already exist");
            }

            var lga = await _unitOfWork.LgaRepo.GetFirstOrDefaultAsync(filter: x=>x.Id == request.LGAId);
            try
            {
                await _unitOfWork.BeginTransaction();
                var newCustomer = new Customer
                {
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    Password = request.Password, // password will be hashed 
                    StateOfResidenceId = request.StateOfResidenceId,
                    LGAId = request.LGAId,
                    ProgressState = ProgressState.Initiated,
                };

                _unitOfWork.CustomerRepo.Add(newCustomer);
                await _unitOfWork.SaveChangesAsync();


                //Generate and send email verification Otp
                var emailOtp = RandomNumberGenerator.DigitGen();
                var otp = new OTPEntity
                {
                    UserId = newCustomer.Id.ToString(),
                    Email = newCustomer.Email,
                    ExpiryDate = DateTime.Now.AddMinutes(5),
                    OTP = emailOtp,
                    OtpChannel = OtpChannel.Email,
                    OtpType = OtpType.EmailVerification,
                };
                _unitOfWork.Otp.Add(otp);
                await _unitOfWork.SaveChangesAsync();

                //Send otp by calling sms service
                await Task.Delay(1000);

                await _unitOfWork.CommitTransaction();
                return ResponseHandler.SuccessResponse($"Otp has bee sent to {request.PhoneNumber}", new { otpId = otp.Id, otp = otp.OTP });
            }
            catch (Exception)
            {
               await _unitOfWork.RollBack();
                return ResponseHandler.FailureResponse("500", "An error occured while creating customer");
            }

        }

        public async Task<ApiResponse> GetCustomers(int pageNumber, int pageSize)
        {
            var customers = await _unitOfWork.CustomerRepo.GetAll();

            var customersCount = customers.Count();
            // Get the paginated list of customers
            var PaginatedCustomer = customers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Create a response object
            var response = new
            {
                TotalCount = customersCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Customers = customers.Adapt<List<CustomerResponse>>()
            };
            return ResponseHandler.SuccessResponse("Customers retrieved successfully", response);
        }

        public async Task<ApiResponse> GetLgaByStateId(int stateId)
        {
            var lga = await _unitOfWork.LgaRepo.GetAllAsync(filter: x=>x.StateId == stateId);
            return ResponseHandler.SuccessResponse("LGA retrieved Successfully ", lga);
        }

        public async Task<ApiResponse> GetState()
        {
            var states = await _unitOfWork.stateRepo.GetAll();
            var response = new
            {
                TotalCount = states.Count(),
                States = states.Adapt<List<object>>()
            };
            return ResponseHandler.SuccessResponse("States Retrieved Successfully", response);
        }

        public async Task<ApiResponse> VerifyOtp(VerifyOtpRequest request)
        {
            var otp = await _unitOfWork.Otp.GetFirstOrDefaultAsync(filter: x=>x.Id == request.OtpId);
            if (otp == null)
            {
                return ResponseHandler.FailureResponse("400", "Invalid Otp");
            }
            if(otp.OTP != request.Otp)
            {
                return ResponseHandler.FailureResponse("400", "Invalid Otp");
            }
            if (otp.ExpiryDate < DateTime.Now)
            {
                return ResponseHandler.FailureResponse("400", "Otp Expired");
            }
            if (otp.Status != OtpStatus.Active)
            {
                return ResponseHandler.FailureResponse("400", "Otp has been used");
            }
            var customer = await _unitOfWork.CustomerRepo.GetFirstOrDefaultAsync(filter: x=>x.Id == int.Parse(otp.UserId));
            customer.IsVerified = true;
            customer.ProgressState = ProgressState.Completed;
            otp.Status = OtpStatus.Verified;
            _unitOfWork.Otp.Update(otp);
            await _unitOfWork.SaveChangesAsync();
             _unitOfWork.CustomerRepo.Update(customer);
            await _unitOfWork.SaveChangesAsync();
            return ResponseHandler.SuccessResponse("Otp Verified Successfully");
        }
    }
}