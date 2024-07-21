using Airline.ModelsService.Models;
using App.Areas.Identity.Controllers;
using App.Areas.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Xunit;

namespace Testing.xUnit.Controllers.Identity
{
    public class AccountControllerTest
    {
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<SignInManager<AppUser>> _mockSignInManager;
        private readonly Mock<IEmailSender> _mockEmailSender;
        private readonly Mock<ILogger<AccountController>> _mockLogger;
        private readonly AccountController _controller;

        public AccountControllerTest()
        {
            _mockUserManager = MockUserManager<AppUser>();
            _mockSignInManager = MockSignInManager<AppUser>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockLogger = new Mock<ILogger<AccountController>>();

            _controller = new AccountController(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _mockEmailSender.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task Login_InvalidUser_ReturnsViewWithModelError()
        {
            // Arrange
            var mockUserStore = new Mock<IUserStore<AppUser>>();
            var mockUserManager = new Mock<UserManager<AppUser>>(
                mockUserStore.Object,
                null, null, null, null, null, null, null, null);

            var mockSignInManager = new Mock<SignInManager<AppUser>>(
                mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
                null, null, null, null);

            var mockLogger = new Mock<ILogger<AccountController>>();
            var mockEmailSender = new Mock<IEmailSender>();

            // Mock the UrlHelper
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.Content(It.IsAny<string>())).Returns<string>(s => s);

            // Create the controller and set the Url property
            var controller = new AccountController(mockUserManager.Object, mockSignInManager.Object, mockEmailSender.Object, mockLogger.Object)
            {
                Url = mockUrlHelper.Object
            };

            var loginViewModel = new LoginViewModel
            {
                UserNameOrEmail = "invaliduser@example.com",
                Password = "InvalidPassword",
                RememberMe = false
            };

            // Simulate an unsuccessful login attempt
            mockSignInManager
                .Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Simulate that user is not found
            mockUserManager
                .Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((AppUser)null);

            // Act
            var result = await controller.Login(loginViewModel, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var modelState = viewResult.ViewData.ModelState;

            Assert.False(modelState.IsValid);
            Assert.True(modelState.ContainsKey(string.Empty));
            Assert.Equal("Không đăng nhập được.", modelState[string.Empty].Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Login_ValidUser_RedirectsToReturnUrl()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<AppUser>>(
                Mock.Of<IUserStore<AppUser>>(),
                null, null, null, null, null, null, null, null);

            var mockSignInManager = new Mock<SignInManager<AppUser>>(
                mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
                null, null, null, null);

            var mockLogger = new Mock<ILogger<AccountController>>();
            var mockEmailSender = new Mock<IEmailSender>();

            var controller = new AccountController(mockUserManager.Object, mockSignInManager.Object, mockEmailSender.Object, mockLogger.Object);

            var loginViewModel = new LoginViewModel
            {
                UserNameOrEmail = "validuser@example.com",
                Password = "ValidPassword",
                RememberMe = false
            };

            // Simulate a successful login attempt
            mockSignInManager
                .Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await controller.Login(loginViewModel, "/home");

            // Assert
            var redirectResult = Assert.IsType<LocalRedirectResult>(result);
            Assert.Equal("/home", redirectResult.Url);
        }

        [Fact]
        public async Task Login_ValidEmailButInvalidPassword_ReturnsViewWithModelError()
        {
            // Arrange
            var mockUserStore = new Mock<IUserStore<AppUser>>();
            var mockUserManager = new Mock<UserManager<AppUser>>(
                mockUserStore.Object,
                null, null, null, null, null, null, null, null);

            var mockSignInManager = new Mock<SignInManager<AppUser>>(
                mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
                null, null, null, null);

            var mockLogger = new Mock<ILogger<AccountController>>();
            var mockEmailSender = new Mock<IEmailSender>();

            // Mock the UrlHelper
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.Content(It.IsAny<string>())).Returns<string>(s => s);

            // Create the controller and set the Url property
            var controller = new AccountController(mockUserManager.Object, mockSignInManager.Object, mockEmailSender.Object, mockLogger.Object)
            {
                Url = mockUrlHelper.Object
            };

            var loginViewModel = new LoginViewModel
            {
                UserNameOrEmail = "user@example.com",
                Password = "InvalidPassword",
                RememberMe = false
            };

            // Simulate a failed login attempt with email
            mockSignInManager
                .Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Simulate finding a user by email
            var user = new AppUser { UserName = "user" };
            mockUserManager
                .Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Simulate a failed login attempt with username
            mockSignInManager
                .Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            var result = await controller.Login(loginViewModel, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var modelState = viewResult.ViewData.ModelState;

            Assert.False(modelState.IsValid);
            Assert.True(modelState.ContainsKey(string.Empty));
            Assert.Equal("Không đăng nhập được.", modelState[string.Empty].Errors.First().ErrorMessage);
        }


        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(
                store.Object, null, null, null, null, null, null, null, null);
        }

        private static Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
        {
            var userManager = MockUserManager<TUser>().Object;
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<TUser>>();
            return new Mock<SignInManager<TUser>>(
                userManager, contextAccessor.Object, claimsFactory.Object, null, null, null, null);
        }
    }
}
