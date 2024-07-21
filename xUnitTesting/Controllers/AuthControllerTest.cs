using Airline.ModelsService.Models.DTOs.Auth;
using Airline.Services.AuthAPI.Controllers;
using Airline.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;
using static Airline.Services.AuthAPI.Responses.ServiceResponses;

namespace Testing.xUnit.Controllers
{
    public class AuthControllerTest 
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _authController;
        public AuthControllerTest()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);

            var admin = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Administrator")
            }, "mock"));

            // Setup HttpContext with authenticated user
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, "f671b58d-8f3f-4416-95bc-48e09d26dd2d"),
            new Claim(ClaimTypes.Name, "nhatha"),
            }, "TestAuthentication"));

            _authController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = admin }
            };

            _authController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }

        [Fact]
        public async Task GetUser_ReturnsNotFoundResult_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            var response = new AccountResponse(false, "User not found", null);
            _mockAuthService.Setup(service => service.GetUser(userId)).ReturnsAsync(response);

            // Act
            var result = await _authController.GetUser(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetUser_ReturnsOkResult_WhenUserExists()
        {
            // Arrange
            var userId = "f671b58d-8f3f-4416-95bc-48e09d26dd2d";
            var user = new AccountDTO { Id = userId, UserName = "nhatha" };
            var response = new AccountResponse(true, "User found", new List<AccountDTO> { user });
            _mockAuthService.Setup(service => service.GetUser(userId)).ReturnsAsync(response);

            // Act
            var result = await _authController.GetUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AccountResponse>(okResult.Value);
            Assert.Equal(response.Message, returnValue.Message);
            Assert.True(returnValue.flag);
            Assert.Single(returnValue.AccountDTO);
            Assert.Equal(userId, returnValue.AccountDTO.First().Id);
        }

        [Fact]
        public async Task RegisterMember_ReturnsOkResult_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var registerDTO = new RegisterDTO { UserName = "testuser", Email = "test@example.com", Password = "Test@1234" };
            var response = new GeneralResponse(true, "User registered successfully");
            _mockAuthService.Setup(service => service.RegisterMemberAccount(registerDTO)).ReturnsAsync(response);

            // Act
            var result = await _authController.RegisterMember(registerDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response.Message, okResult.Value);
        }

        [Fact]
        public async Task RegisterMember_ReturnsBadRequestResult_WhenRegistrationFails()
        {
            // Arrange
            var registerDTO = new RegisterDTO { UserName = "testuser", Email = "test@example.com", Password = "Test@1234" };
            var response = new GeneralResponse(false, "Registration failed");
            _mockAuthService.Setup(service => service.RegisterMemberAccount(registerDTO)).ReturnsAsync(response);

            // Act
            var result = await _authController.RegisterMember(registerDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(response.Message, badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginDTO = new LoginDTO { Email = "test@example.com", Password = "Test@1234" };
            var response = new LoginResponse(true, "dummyToken", "Login successful");
            _mockAuthService.Setup(service => service.LoginAccount(loginDTO)).ReturnsAsync(response);

            // Act
            var result = await _authController.Login(loginDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsBadRequestResult_WhenLoginFails()
        {
            // Arrange
            var loginDTO = new LoginDTO { Email = "test@example.com", Password = "Test@1234" };
            var response = new LoginResponse(false, null!, "Login failed");
            _mockAuthService.Setup(service => service.LoginAccount(loginDTO)).ReturnsAsync(response);

            // Act
            var result = await _authController.Login(loginDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(response.Message, badRequestResult.Value);
        }

        [Fact]
        public async Task GetCurrentUser_ReturnsOkResult_WhenUserExists()
        {
            // Arrange
            var userId = "f671b58d-8f3f-4416-95bc-48e09d26dd2d";
            var user = new AccountDTO { Id = userId, UserName = "nhatha" };
            var response = new AccountResponse(true, "User found", new List<AccountDTO> { user });
            _mockAuthService.Setup(service => service.GetCurrentUser(userId)).ReturnsAsync(response);

            // Act
            var result = await _authController.GetCurrentUser();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AccountDTO>(okResult.Value);
            Assert.Equal(userId, returnValue.Id);
            Assert.Equal("nhatha", returnValue.UserName);
        }

        [Fact]
        public async Task GetCurrentUser_ReturnsNotFoundResult_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "f671b58d-8f3f-4416-95bc-48e09d26dd2d";
            var response = new AccountResponse(false, "User not found", null);
            _mockAuthService.Setup(service => service.GetCurrentUser(userId)).ReturnsAsync(response);

            // Act
            var result = await _authController.GetCurrentUser();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User not found", notFoundResult.Value);
        }
    }
}
