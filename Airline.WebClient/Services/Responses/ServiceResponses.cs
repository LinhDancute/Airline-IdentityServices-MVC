using Airline.WebClient.Models.DTOs.Auth;

namespace Airline.WebClient.Services.Responses
{
    public class ServiceResponses
    {
        public record class GeneralResponse(bool flag, string Message);
        public record LoginResponse(bool flag, string Token, string Message)
        {
            public bool IsSuccess => flag;
        }
        public record class AccountResponse(bool flag, string Message, List<AccountDTO> AccountDTO); 
    }
}
