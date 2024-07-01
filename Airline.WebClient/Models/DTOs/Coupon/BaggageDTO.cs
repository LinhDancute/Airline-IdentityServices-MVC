using Airline.WebClient.Models.Airline;

namespace Airline.WebClient.Models.DTOs.Coupon
{
    public class BaggageDTO
    {
        public int BaggageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Ticket>? Tickets { get; } = new List<Ticket>();
        public ICollection<TicketClass_Baggage>? TicketClass_Baggages { get; set; } = new List<TicketClass_Baggage>();
    }
}
