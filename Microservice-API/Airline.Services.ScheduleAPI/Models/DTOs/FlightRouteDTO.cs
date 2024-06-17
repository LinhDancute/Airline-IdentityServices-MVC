using static Airline.Services.ScheduleAPI.Models.FlightRoute;

namespace Airline.Services.ScheduleAPI.Models.DTOs
{
    public class FlightRouteDTO
    {
        public int FlightRouteId { get; set; }
        public string DepartureAddress { get; set; }
        public string ArrivalAddress { get; set; }
        public string? FlightSector { get; set; }
        public string? FlightSectorName { get; set; }
        public GateStatus Gate { get; set; }
        public FlightRouteStatus Status { get; set; }
        public ICollection<int> AirportIDs { get; set; } //Airport IDs for FlightRoute_Airport
        public ICollection<int> FlightIDs { get; set; } //Flight IDs for FlightRoute_Flight

        public enum GateStatus
        {
            DomesticGate,
            InternationalGate
        }

        public enum FlightRouteStatus
        {
            Active,
            Inactive
        }
    }
}
