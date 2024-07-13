using AlatAuth.Business.Service.Implementation;
using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.Entity;
using AlatAuth.Data.Interface;
using Moq;
using System.Linq.Expressions;

namespace AlatAuth.Business.Tests.Customer_Service
{
    public class GetCustomerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICustomerRepo> _mockCustomerRepo;
        private readonly CustomerService _customerService;

        public GetCustomerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCustomerRepo = new Mock<ICustomerRepo>();
            _mockUnitOfWork.Setup(uow => uow.CustomerRepo).Returns(_mockCustomerRepo.Object);
            _customerService = new CustomerService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetCustomers_ReturnsPaginatedCustomersSuccessfully()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 2;
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FullName = "John Doe" },
                new Customer { Id = 2, FullName = "Jane Doe" },
                new Customer { Id = 3, FullName = "Sam Smith" }
            };

            _mockCustomerRepo.Setup(repo => repo.GetAllAsyncByDesc(It.IsAny<Expression<Func<Customer, bool>>>(), It.IsAny<string>(),
                 It.IsAny<Expression<Func<Customer, bool>>>(),  false,  0)).ReturnsAsync(customers.ToList());

            // Act
            var response = await _customerService.GetCustomers(pageNumber, pageSize);

            // Assert
            Assert.Equal("00",response.StatusCode);
            Assert.Equal("Customers retrieved successfully", response.Message);
            Assert.NotNull(response.Data);

           
        }
    }
}
