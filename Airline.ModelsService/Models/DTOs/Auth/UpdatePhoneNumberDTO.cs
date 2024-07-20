using System.ComponentModel.DataAnnotations;

namespace Airline.ModelsService.Models.DTOs.Auth
{
    public class UpdatePhoneNumberDTO
    {
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
