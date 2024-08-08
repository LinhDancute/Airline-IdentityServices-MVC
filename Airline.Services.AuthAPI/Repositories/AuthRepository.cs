using Microsoft.AspNetCore.Identity;
using Airline.Services.AuthAPI.Services;
using Airline.Services.AuthAPI.Responses;
using static Airline.Services.AuthAPI.Responses.ServiceResponses;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Airline.ModelsService.Models;
using Airline.ModelsService.Models.DTOs.Auth;
using Microsoft.Extensions.Configuration;

namespace Airline.Services.AuthAPI.Repositories
{
    public class AuthRepository : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return new LoginResponse(false, null!, "Login container is empty");
            }

            var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser == null)
            {
                return new LoginResponse(false, null!, "User not found");
            }

            bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
            {
                return new LoginResponse(false, null!, "Invalid email/password");
            }

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new AccountSession(getUser.Id, getUser.UserName, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);

            return new LoginResponse(true, token!, "Login success");
        }

        private string GenerateToken(AccountSession userSession)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userSession.Id),
                new Claim(ClaimTypes.Name, userSession.UserName),
                new Claim(ClaimTypes.Email, userSession.Email),
                new Claim(ClaimTypes.Role, userSession.Role)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<GeneralResponse> RegisterMemberAccount(RegisterDTO users)
        {
            return await RegisterAccount(users, "Member");
        }

        public async Task<GeneralResponse> RegisterAdminAccount(RegisterDTO users)
        {
            return await RegisterAccount(users, "Administrator");
        }

        private async Task<GeneralResponse> RegisterAccount(RegisterDTO users, string role)
        {
            if (users == null)
            {
                return new GeneralResponse(false, "Model is empty");
            }

            var newUser = new AppUser
            {
                UserName = users.UserName,
                Email = users.Email,
                EmailConfirmed = true
            };

            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
            {
                return new GeneralResponse(false, "User already registered");
            }

            var registerUser = await _userManager.CreateAsync(newUser, users.Password);
            if (!registerUser.Succeeded)
            {
                var errorMessage = string.Join(", ", registerUser.Errors.Select(e => e.Description));
                return new GeneralResponse(false, $"Error creating user: {errorMessage}");
            }

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
                if (!roleResult.Succeeded)
                {
                    var roleErrorMessage = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    return new GeneralResponse(false, $"Error creating role {role}: {roleErrorMessage}");
                }
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(newUser, role);
            if (!addToRoleResult.Succeeded)
            {
                var addToRoleErrorMessage = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                return new GeneralResponse(false, $"Error adding user to role: {addToRoleErrorMessage}");
            }

            return new GeneralResponse(true, $"User registered as {role}");
        }

        public async Task<AccountResponse> GetUser(string id)
        {
            var users = new List<AccountDTO>();

            var accounts = await _userManager.GetUsersInRoleAsync("Member");
            foreach (var account in accounts)
            {
                var roles = await _userManager.GetRolesAsync(account);
                var accountDTO = new AccountDTO
                {
                    Id = account.Id,
                    UserName = account.UserName,
                    Email = account.Email,
                    HomeAddress = account.HomeAddress,
                    PhoneNumber = account.PhoneNumber,
                    BirthDate = account.BirthDate,
                    CMND = account.CMND,
                };

                users.Add(accountDTO);
            }

            if (users.Any())
            {
                return new AccountResponse(true, "Users found.", users);
            }
            else
            {
                return new AccountResponse(false, "No users found.", new List<AccountDTO>());
            }
        }

        public async Task<AccountResponse> GetAdmin(string id)
        {
            var admins = new List<AccountDTO>();

            var accounts = await _userManager.GetUsersInRoleAsync("Administrator");
            foreach (var account in accounts)
            {
                var roles = await _userManager.GetRolesAsync(account);
                var accountDTO = new AccountDTO
                {
                    Id = account.Id,
                    UserName = account.UserName,
                    Email = account.Email,
                    HomeAddress = account.HomeAddress,
                    PhoneNumber = account.PhoneNumber,
                    BirthDate = account.BirthDate,
                    CMND = account.CMND,
                };

                admins.Add(accountDTO);
            }

            if (admins.Any())
            {
                return new AccountResponse(true, "Admin users found.", admins);
            }
            else
            {
                return new AccountResponse(false, "No admin users found.", new List<AccountDTO>());
            }
        }

        public async Task<AccountResponse> GetCurrentUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new AccountResponse(false, "User not found.", new List<AccountDTO>());
            }

            var roles = await _userManager.GetRolesAsync(user);
            var accountDTO = new AccountDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                HomeAddress = user.HomeAddress,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                CMND = user.CMND,
                Roles = roles.ToList()
            };

            return new AccountResponse(true, "User found.", new List<AccountDTO> { accountDTO });
        }

        public async Task<GeneralResponse> UpdatePhoneNumber(string userId, string newPhoneNumber)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new GeneralResponse(false, "User not found.");
            }

            user.PhoneNumber = newPhoneNumber;
            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                return new GeneralResponse(true, "Phone number updated successfully.");
            }
            else
            {
                var errorMessage = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                return new GeneralResponse(false, $"Error updating phone number: {errorMessage}");
            }
        }
    }
}
