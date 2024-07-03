using Airline.WebClient.Models.Airline;

namespace Airline.WebClient.Models.DTOs.Coupon
{
    public class UnitPriceDTO
    {
        public int PriceId { get; set; }
        public decimal USD { get; set; }
        public decimal VND { get; set; }
        public ICollection<Ticket> Tickets { get; } = new List<Ticket>();
    }
}
