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
        public int MealId { get; set; }
        public int BaggageId { get; set; }
        public int ClassId { get; set; }
        public string PassengerName { get; set; }
        public string PassengerPhoneNumber { get; set; }
        public string Itinerary { get; set; }
        public string FlightNumber { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string Seat { get; set; }
        public string Class { get; set; }
        public string PNR { get; set; }
        public string MealRequest { get; set; }
        public TicketStatus Status { get; set; }
        public AppUser? Passenger { set; get; }
        public Flight Flight { get; set; }
        public UnitPrice UnitPrice { get; set; }
        public TicketClass TicketClass { get; set; }
        public Meal Meal { get; set; }
        public Baggage Baggage { get; set; }
        public ICollection<BoardingPass>? BoardingPasses { get; } = new List<BoardingPass>();
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
