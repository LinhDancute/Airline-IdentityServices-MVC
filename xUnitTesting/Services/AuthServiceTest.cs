using Airline.ModelsService.Models.DTOs.Auth;
using Airline.Services.AuthAPI.Services;
using Moq;
using Xunit;
using static Airline.Services.AuthAPI.Responses.ServiceResponses;

namespace xUnitTesting.Services
{
    public class AuthServiceTest
    {
        private readonly Mock<IAuthService> _mockAuthService;

        public AuthServiceTest()
        {
            _mockAuthService = new Mock<IAuthService>();
        }

        [Fact]
        public async Task RegisterMemberAccount_ReturnsSuccess_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registerDTO = new RegisterDTO
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "P@ssw0rd"
            };

            var expectedResponse = new GeneralResponse(true, "User registered as Member");

            _mockAuthService.Setup(service => service.RegisterMemberAccount(registerDTO))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockAuthService.Object.RegisterMemberAccount(registerDTO);

            // Assert
            Assert.True(result.flag);
            Assert.Equal("User registered as Member", result.Message);
        }

        [Fact]
        public async Task RegisterMemberAccount_ReturnsError_WhenUserAlreadyExists()
        {
            // Arrange
            var registerDTO = new RegisterDTO
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "P@ssw0rd"
            };

            var expectedResponse = new GeneralResponse(false, "User already registered");

            _mockAuthService.Setup(service => service.RegisterMemberAccount(registerDTO))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockAuthService.Object.RegisterMemberAccount(registerDTO);

            // Assert
            Assert.False(result.flag);
            Assert.Equal("User already registered", result.Message);
        }
    }
}
