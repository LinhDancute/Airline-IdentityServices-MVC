using Airline.WebClient.Models.Airline;

namespace Airline.WebClient.Models.DTOs.Coupon
{
    public class MealDTO
    {
        public int MealId { get; set; }
        public string MealCode { get; set; }
        public string Desciption { get; set; }
        public ICollection<Ticket>? Tickets { get; } = new List<Ticket>();
    }
}
