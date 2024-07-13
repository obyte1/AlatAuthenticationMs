using AlatAuth.Business.Interface;
using AlatAuth.Business.Service.Implementation;
using AlatAuth.Business.Service.Interface;
using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.Dto;
using AlatAuth.Data.Entity;
using AlatAuth.Data.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Business.Tests.Customer_Service
{
    public class CreateCustomerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICustomerRepo> _customerRepoMock;
        private readonly Mock<ILgaRepo> _lgaRepoMock;
        private readonly Mock<IOtpRepo> _otpRepoMock;
        private readonly CustomerService _service;

        public CreateCustomerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _customerRepoMock = new Mock<ICustomerRepo>();
            _lgaRepoMock = new Mock<ILgaRepo>();
            _otpRepoMock = new Mock<IOtpRepo>();

            _unitOfWorkMock.Setup(u => u.CustomerRepo).Returns(_customerRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.LgaRepo).Returns(_lgaRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.Otp).Returns(_otpRepoMock.Object);

            _service = new CustomerService(_unitOfWorkMock.Object); // Replace YourService with the actual service name
        }

        [Fact]
        public async Task CreateCustomer_ReturnsFailure_WhenPhoneNumberExists()
        {
            // Arrange
            var request = new CustomerRequest { PhoneNumber = "1234567890", Email = "test@example.com" };
            _customerRepoMock.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(true); // Si

            // Act
            var result = await _service.CreateCustomer(request);

            // Assert
            Assert.Equal("400", result.StatusCode);
            Assert.Equal("Phone number already Exist", result.Message);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsFailure_WhenEmailExists()
        {
            // Arrange
            var request = new CustomerRequest { Email = "test@example.com" };
            _customerRepoMock.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(false); // Phone number check passes
            _customerRepoMock.Setup(r => r.GetFirstOrDefaultAsync("", It.IsAny<Expression<Func<Customer, bool>>>()))
                             .ReturnsAsync(new Customer()); // Email check fails

            // Act
            var result = await _service.CreateCustomer(request);

            // Assert
            Assert.Equal("400", result.StatusCode);
            Assert.Equal("Email already exist", result.Message);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsSuccess_WhenCustomerIsCreated()
        {
            // Arrange
            var request = new CustomerRequest { PhoneNumber = "1234567890", Email = "test@example.com", LGAId = 1 };
           

            // Act
            _customerRepoMock.Setup(repo => repo.AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                       .ReturnsAsync(false); // Phone number check passes
            _customerRepoMock.Setup(r => r.GetFirstOrDefaultAsync("", It.IsAny<Expression<Func<Customer, bool>>>()))
                             .ReturnsAsync((Customer)null!); // Email check fails
            var result = await _service.CreateCustomer(request);

            // Assert
            Assert.Equal("Successful", result.Status); // Adjust based on actual success code
            Assert.Contains("Otp has bee sent to 1234567890", result.Message);
        }

    }
}
