using Airline.ModelsService.Models.DTOs.Auth;
using static Airline.Services.AuthAPI.Responses.ServiceResponses;

namespace Airline.Services.AuthAPI.Services
{
    public interface IAuthService
    {
        Task<GeneralResponse> RegisterAccount(RegisterDTO users, bool isAdmin);
        Task<LoginResponse> LoginAccount(LoginDTO loginUser);
        Task <AccountResponse> GetAdmin(string id);
        Task<AccountResponse> GetUser(string id);
    }
}
