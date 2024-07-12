using Airline.ModelsService.Models.DTOs.Schedule;

namespace Airline.ModelsService.Models.DTOs.Schedule
{
    public class FlightCreateDTO
    {
        public DateTime Date { get; set; }
        public string FlightNumber { get; set; }
        public string FlightSector { get; set; }
        public string Aircraft { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public float FlightTime { get; set; }
        public int? EconomySeat { get; set; }
        public int? PremiumEconomySeat { get; set; }
        public int? BusinessSeat { get; set; }
        public FlightDTO.FlightStatus Status { get; set; }
    }
}
