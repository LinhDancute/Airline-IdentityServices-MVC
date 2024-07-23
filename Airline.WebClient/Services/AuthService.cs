using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Models.DTOs.Auth;
using Airline.WebClient.Services.IServices;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static Airline.WebClient.Services.Responses.ServiceResponses;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<GeneralResponse> RegisterMemberAccount(RegisterDTO users)
        {
            var response = await _baseService.SendAsyncAuthAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = $"{AuthAPIBase}/api/Auth/register/member",
                Data = users
            });

            return ProcessGeneralResponse(response);
        }

        public async Task<GeneralResponse> RegisterAdminAccount(RegisterDTO users)
        {
            var response = await _baseService.SendAsyncAuthAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = $"{AuthAPIBase}/api/Auth/register/admin",
                Data = users
            });

            return ProcessGeneralResponse(response);
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginUser)
        {
            var response = await _baseService.SendAsyncAuthAPI(new RequestDTO
            {
                ApiType = ApiType.POST,
                ApiUrl = $"{AuthAPIBase}/api/Auth/login",
                Data = loginUser
            });

            return ProcessLoginResponse(response);
        }

        public async Task<AccountResponse> GetAdmin(string id)
        {
            var response = await _baseService.SendAsyncAuthAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = $"{AuthAPIBase}/api/Auth/admin/{id}"
            });

            return ProcessAccountResponse(response);
        }

        public async Task<AccountResponse> GetUser(string id)
        {
            var response = await _baseService.SendAsyncAuthAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = $"{AuthAPIBase}/api/Auth/user/{id}"
            });

            return ProcessAccountResponse(response);
        }

        public async Task<AccountResponse> GetCurrentUser(string id)
        {
            var response = await _baseService.SendAsyncAuthAPI(new RequestDTO
            {
                ApiType = ApiType.GET,
                ApiUrl = $"{AuthAPIBase}/api/Auth/currentUser/{id}"
            });

            return ProcessAccountResponse(response);
        }

        public async Task<GeneralResponse> UpdatePhoneNumber(string userId, string newPhoneNumber)
        {
            var response = await _baseService.SendAsyncAuthAPI(new RequestDTO
            {
                ApiType = ApiType.PUT,
                ApiUrl = $"{AuthAPIBase}/api/Auth/updatePhoneNumber",
                Data = new { userId, newPhoneNumber }
            });

            return ProcessGeneralResponse(response);
        }

        private GeneralResponse ProcessGeneralResponse(ResponseDTO response)
        {
            if (response != null && response.IsSuccess)
            {
                return JsonConvert.DeserializeObject<GeneralResponse>(Convert.ToString(response.Result));
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new GeneralResponse(false, response?.Message);
            }
        }

        private LoginResponse ProcessLoginResponse(ResponseDTO response)
        {
            if (response != null && response.IsSuccess)
            {
                return JsonConvert.DeserializeObject<LoginResponse>(Convert.ToString(response.Result));
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new LoginResponse(false, null, response?.Message);
            }
        }

        private AccountResponse ProcessAccountResponse(ResponseDTO response)
        {
            if (response != null && response.IsSuccess)
            {
                return JsonConvert.DeserializeObject<AccountResponse>(Convert.ToString(response.Result));
            }
            else
            {
                Console.WriteLine($"Request failed with message: {response?.Message}");
                return new AccountResponse(false, response?.Message, null);
            }
        }
    }
}
