using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Services.ScheduleAPI.Models.DTOs
{
    public class FlightDTO
    {
        public int FlightId { get; set; }

        public int? AirlineId { get; set; }

        public string? Aircraft { set; get; }

        public string? FlightNumber { get; set; }

        public string? FlightSector { get; set; }

        public float? FlightTime { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public DateTime Date { get; set; }
        public int? EcoSeat { get; set; }
        public int? DeluxeSeat { get; set; }

        public int? SkyBossSeat { get; set; }

        public int? SkyBossBusinessSeat { get; set; }
        public Airline? Airline { get; set; }
        public ICollection<FlightRoute_Flight>? FlightRoute_Flights { get; set; } = new List<FlightRoute_Flight>();

        public StatusType Status { get; set; }

        public enum StatusType
        {
            Active,
            Inactive
        }
    }
}