using Airline.ModelsService.Models;
using Airline.ModelsService.Models.DTOs.Auth;
using Airline.Services.AuthAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace xUnitTesting.Repositories
{
    public class AuthRepositoryTest
    {
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly AuthRepository _authRepository;

        public AuthRepositoryTest()
        {
            _mockUserManager = new Mock<UserManager<AppUser>>(
                Mock.Of<IUserStore<AppUser>>(),
                null, null, null, null, null, null, null, null);

            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                Mock.Of<IRoleStore<IdentityRole>>(),
                null, null, null, null);

            _mockConfiguration = new Mock<IConfiguration>();

            _authRepository = new AuthRepository(
                _mockUserManager.Object,
                _mockRoleManager.Object,
                _mockConfiguration.Object);
        }

        [Fact]
        public async Task GetCurrentUser_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = "f671b58d-8f3f-4416-95bc-48e09d26dd2d";
            var user = new AppUser
            {
                Id = userId,
                UserName = "nhatha",
                Email = "nhatha@example.com",
                HomeAddress = "123 Main St",
                BirthDate = new DateTime(1990, 1, 1),
                PhoneNumber = "0798268775",
                CMND = "123456789",
            };

            _mockUserManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync(user);

            _mockUserManager.Setup(um => um.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "Member" });

            // Act
            var result = await _authRepository.GetCurrentUser(userId);

            // Assert
            var accountDTO = Assert.Single(result.AccountDTO);
            Assert.True(result.flag);
            Assert.Equal("User found.", result.Message);
            Assert.Equal(userId, accountDTO.Id);
            Assert.Equal("nhatha", accountDTO.UserName);
            Assert.Equal("nhatha@example.com", accountDTO.Email);
            Assert.Equal("123 Main St", accountDTO.HomeAddress);
            Assert.Equal("0798268775", accountDTO.PhoneNumber);
            Assert.Equal("123456789", accountDTO.CMND);
            Assert.Contains("Member", accountDTO.Roles);
        }

        [Fact]
        public async Task GetCurrentUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "non-existent-user-id";
            _mockUserManager.Setup(um => um.FindByIdAsync(userId))
                .ReturnsAsync((AppUser)null);

            // Act
            var result = await _authRepository.GetCurrentUser(userId);

            // Assert
            Assert.False(result.flag);
            Assert.Equal("User not found.", result.Message);
            Assert.Null(result.AccountDTO);
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

            _mockUserManager.Setup(um => um.FindByEmailAsync(registerDTO.Email))
                .ReturnsAsync((AppUser)null); // User not found

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), registerDTO.Password))
                .ReturnsAsync(IdentityResult.Success);

            _mockRoleManager.Setup(rm => rm.RoleExistsAsync("Member"))
                .ReturnsAsync(true); // Role exists

            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), "Member"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authRepository.RegisterMemberAccount(registerDTO);

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

            _mockUserManager.Setup(um => um.FindByEmailAsync(registerDTO.Email))
                .ReturnsAsync(new AppUser()); // User already exists

            // Act
            var result = await _authRepository.RegisterMemberAccount(registerDTO);

            // Assert
            Assert.False(result.flag);
            Assert.Equal("User already registered", result.Message);
        }

        [Fact]
        public async Task RegisterMemberAccount_ReturnsError_WhenRoleCreationFails()
        {
            // Arrange
            var registerDTO = new RegisterDTO
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "P@ssw0rd"
            };

            _mockUserManager.Setup(um => um.FindByEmailAsync(registerDTO.Email))
                .ReturnsAsync((AppUser)null); // User not found

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), registerDTO.Password))
                .ReturnsAsync(IdentityResult.Success);

            _mockRoleManager.Setup(rm => rm.RoleExistsAsync("Member"))
                .ReturnsAsync(false); // Role does not exist

            _mockRoleManager.Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Role creation failed" }));

            // Act
            var result = await _authRepository.RegisterMemberAccount(registerDTO);

            // Assert
            Assert.False(result.flag);
            Assert.StartsWith("Error creating role Member:", result.Message);
        }

        [Fact]
        public async Task RegisterMemberAccount_ReturnsError_WhenAddToRoleFails()
        {
            // Arrange
            var registerDTO = new RegisterDTO
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "P@ssw0rd"
            };

            _mockUserManager.Setup(um => um.FindByEmailAsync(registerDTO.Email))
                .ReturnsAsync((AppUser)null); // User not found

            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), registerDTO.Password))
                .ReturnsAsync(IdentityResult.Success);

            _mockRoleManager.Setup(rm => rm.RoleExistsAsync("Member"))
                .ReturnsAsync(true); // Role exists

            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), "Member"))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to add to role" }));

            // Act
            var result = await _authRepository.RegisterMemberAccount(registerDTO);

            // Assert
            Assert.False(result.flag);
            Assert.StartsWith("Error adding user to role:", result.Message);
        }
    }
}
