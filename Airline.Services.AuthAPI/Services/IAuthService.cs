using Airline.ModelsService.Models.DTOs.Auth;
using static Airline.Services.AuthAPI.Responses.ServiceResponses;

namespace Airline.Services.AuthAPI.Services
{
    public interface IAuthService
    {
        Task<GeneralResponse> RegisterMemberAccount(RegisterDTO users);
        Task<GeneralResponse> RegisterAdminAccount(RegisterDTO users);
        Task<LoginResponse> LoginAccount(LoginDTO loginUser);
        Task <AccountResponse> GetAdmin(string id);
        Task<AccountResponse> GetUser(string id);
        Task<AccountResponse> GetCurrentUser(string id);
        Task<GeneralResponse> UpdatePhoneNumber(string userId, string newPhoneNumber);
    }
}
