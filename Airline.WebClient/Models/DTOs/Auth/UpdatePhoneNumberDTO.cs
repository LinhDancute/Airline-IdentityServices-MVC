using System.ComponentModel.DataAnnotations;

namespace Airline.WebClient.Models.DTOs.Auth
{
    public class UpdatePhoneNumberDTO
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
