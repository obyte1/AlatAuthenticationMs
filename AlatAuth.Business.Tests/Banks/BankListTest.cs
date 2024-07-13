using AlatAuth.Business.Adapter;
using AlatAuth.Business.Service.Implementation;
using AlatAuth.Data.Dto;
using AlatAuth.Data.Dto.Banks;
using Microsoft.Extensions.Configuration;
using Moq;

namespace AlatAuth.Business.Tests.Banks
{
    public class BankListTest
    {

        private readonly Mock<IConfiguration> _configurationMock;
        private readonly BankService _bankService;
        private readonly Mock<IGenericAdapter> _adapterMock;

        public BankListTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _adapterMock = new Mock<IGenericAdapter>();

            _bankService = new BankService(
              _adapterMock.Object,
                _configurationMock.Object
            );
        }

        [Fact]
        public async Task GetBanks_ReturnsSuccessResponse_WhenBanksFetchedSuccessfully()
        {
            // Arrange
            var bankResponseDto = new GetBankResponseDto
            {
                result = new List<BankDto> { new BankDto { bankCode = "00", bankName = "Bank A" } }
            };

            // Mock the configuration to return the expected URL
            _configurationMock.Setup(c => c["Alat:AlatBaseUrl"]).Returns("https://example.com/");

            // Mock the SendAsync method to return a successful response

            _adapterMock.Setup(x => x.SendAsync<GetBankResponseDto>(It.IsAny<APIRequestModel>()))
                .ReturnsAsync(bankResponseDto);

            // Act
            var result = await _bankService.GetBanks();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Bank list fetched successfully!", result.Message);
            Assert.Single(((List<BankDto>)result.Data));
            Assert.Equal("Bank A", ((List<BankDto>)result.Data)[0].bankName);
        }


        [Fact]
        public async Task GetBanks_ReturnsErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            _configurationMock.Setup(c => c["Alat:AlatBaseUrl"]).Returns("https://example.com/");

            // Mock the SendAsync method to throw an exception
            _adapterMock
                .Setup(x => x.SendAsync<GetBankResponseDto>(It.IsAny<APIRequestModel>()))
                .ReturnsAsync(new GetBankResponseDto { hasError = true});

            // Act
            var result = await _bankService.GetBanks();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
        }
    }
}
