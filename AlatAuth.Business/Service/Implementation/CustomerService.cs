using AlatAuth.Business.Service.Interface;
using AlatAuth.Common.Enum;
using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.Dto;
using AlatAuth.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AlatAuth.Business.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;


        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<dynamic> CreateCustomer(CustomerRequest request)
        {
            Customer user = null;

            // Check if a customer with the same phone number already exists
            var existingCustomer = await _repository.Find(x => x.PhoneNumber == request.PhoneNumber);
            if (existingCustomer != null)
            {
                return ResponseHandler.FailureResponse("400", "User created successfully");
            }

            // Create a new customer object
            var newCustomer = new Customer
            {
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = request.Password, // password will be hashed 
                StateOfResidence = request.StateOfResidence,
                LGA = request.LGA,
                ProgressState = ProgressState.Initiated,
            };

            var result = await _repository.Add(newCustomer);
            await _unitOfWork.SaveChangesAsync();

            if (result.Succeeded)
            {
                //Generate and send email verification Otp
                var emailOtp = RandomNumberGenerator.DigitGen();
                var otp = new OTPEntity
                {
                    UserId = user.Id.ToString(),
                    Email = user.Email,
                    ExpiryDate = DateTime.Now.AddMinutes(5),
                    OTP = emailOtp,
                    OtpChannel = OtpChannel.Email,
                    OtpType = OtpType.EmailVerification,
                };
                _unitOfWork.Otp.Add(otp);
                await _unitOfWork.SaveChangesAsync();

                var emailText = $"Your email verification code is {emailOtp}";
                var emailSubject = "Email Verification";
                await _emailSender.SendEmailAsync(user.Email, emailSubject, emailText);

                await _unitOfWork.CommitTransaction();
                return ResponseHandler.SuccessResponse("User created successfully", new { otpId = otp.Id });
            }


        }

        public async Task<dynamic> GetCustomers(int pageNumber, int pageSize)
        {
            // Get the total count of customers
            var totalCustomers = await _repository.CountAsync();

            // Get the paginated list of customers
            var customers = await _repository.GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Create a response object
            var response = new
            {
                TotalCount = totalCustomers,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Customers = customers
            };

            return ResponseHandler.SuccessResponse("Customers retrieved successfully", response);
        }
    }
}