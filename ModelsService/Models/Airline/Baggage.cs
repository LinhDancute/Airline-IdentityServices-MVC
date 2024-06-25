using Airline.ModelsService.Models.Airline;

namespace Airline.ModelsService.Models.Airline
{
    public class Baggage
    {
        public int BaggageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BoardingPass>? BoardingPasses { get; } = new List<BoardingPass>();
        public ICollection<Ticket>? Tickets { get; } = new List<Ticket>();
        public ICollection<TicketClass_Baggage>? TicketClass_Baggages { get; set; } = new List<TicketClass_Baggage>();
    }
}
