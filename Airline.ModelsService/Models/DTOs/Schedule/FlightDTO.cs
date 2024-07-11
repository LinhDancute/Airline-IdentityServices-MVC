using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.ModelsService.Models.DTOs.Schedule
{
    public class FlightDTO
    {
        public int FlightId { get; set; }
        public int? AirlineId { get; set; }
        public string Aircraft { get; set; }
        public string FlightNumber { get; set; }
        public string FlightSector { get; set; }
        public float? FlightTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public DateTime Date { get; set; }
        public int? EconomySeat { get; set; }
        public int? PremiumEconomySeat { get; set; }
        public int? BusinessSeat { get; set; }
        public AirlineDTO Airline { get; set; }
        public ICollection<int> FlightRouteIds { get; set; } // FlightRoute_Flight IDs
        public FlightStatus Status { get; set; }

        public enum FlightStatus
        {
            Active,
            Inactive
        }
    }
}