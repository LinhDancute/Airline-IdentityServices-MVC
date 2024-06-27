using Airline.WebClient.Models.DTOs.Schedule;

namespace Airline.WebClient.Models.DTOs.Schedule
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
        public int EcoSeat { get; set; }
        public int DeluxeSeat { get; set; }
        public int SkyBossSeat { get; set; }
        public int SkyBossBusinessSeat { get; set; }
        public FlightDTO.FlightStatus Status { get; set; }
    }
}
