namespace Airline.WebClient.Models.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public AccountDTO User { get; set; }
        public string Token { get; set; }
    }
}
