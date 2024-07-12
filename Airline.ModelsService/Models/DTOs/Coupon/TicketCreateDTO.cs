using Airline.ModelsService.Models.Airline;

namespace Airline.ModelsService.Models.DTOs.Coupon
{
    public class TicketCreateDTO
    {
        public string PassengerName { get; set; }
        public string PassengerPhoneNumber { get; set; }
        public string Itinerary { get; set; }
        public string FlightNumber { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string Seat { get; set; }
        public string Class { get; set; }
        public string PNR { get; set; }
        public List<string> MealRequest { get; set; }
        public List<string> BaggageType { get; set; }
        public string USD { get; set; }
        public string VND { get; set; }
        public TicketStatusType Status { get; set; }
    }
}
