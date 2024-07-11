using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.Statistical;
using System.ComponentModel.DataAnnotations;

namespace Airline.ModelsService.Models.DTOs.Coupon
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public string? PassengerId { set; get; }
        public int FlightId { get; set; }
        public int PriceId { get; set; }
        public int ClassId { get; set; }
        public string PassengerName { get; set; }
        public string PassengerPhoneNumber { get; set; }
        public string Itinerary { get; set; }
        public string FlightNumber { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public string Seat { get; set; }
        public string Class { get; set; }
        public string PNR { get; set; }
        public string MealRequest { get; set; }
        public string BaggageType { get; set; }
        public string USD { get; set; }
        public string VND { get; set; }
        public TicketStatus Status { get; set; }
        public string StatusName { get; set; }
        public AppUser? Passenger { set; get; }
        public Flight Flight { get; set; }
        public UnitPrice UnitPrice { get; set; }
        public TicketClass TicketClass { get; set; }
        public ICollection<BoardingPass>? BoardingPasses { get; } = new List<BoardingPass>();
        public ICollection<Ticket_Meal>? Ticket_Meals { get; set; } = new List<Ticket_Meal>();
        public ICollection<Ticket_Baggage>? Ticket_Baggages { get; set; } = new List<Ticket_Baggage>();
    }

    public enum TicketStatus
    {
        Confirmed,
        Pending,
        Refundable,
        Nonrefundable,
        Cancelled
    }
}
