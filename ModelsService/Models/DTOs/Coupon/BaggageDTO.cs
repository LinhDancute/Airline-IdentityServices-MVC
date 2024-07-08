using Airline.ModelsService.Models.Airline;

namespace Airline.ModelsService.Models.DTOs.Coupon
{
    public class BaggageDTO
    {
        public int BaggageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Ticket_Baggage>? Ticket_Baggages { get; set; }
    }
}
