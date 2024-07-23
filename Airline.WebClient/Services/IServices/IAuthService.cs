using Airline.WebClient.Models.DTOs.Auth;
using static Airline.WebClient.Services.Responses.ServiceResponses;

namespace Airline.WebClient.Services.IServices
{
    public interface IAuthService
    {
        Task<GeneralResponse> RegisterMemberAccount(RegisterDTO users);
        Task<GeneralResponse> RegisterAdminAccount(RegisterDTO users);
        Task<LoginResponse> LoginAccount(LoginDTO loginUser);
        Task<AccountResponse> GetAdmin(string id);
        Task<AccountResponse> GetUser(string id);
        Task<AccountResponse> GetCurrentUser(string id);
        Task<GeneralResponse> UpdatePhoneNumber(string userId, string newPhoneNumber);
    }
}
