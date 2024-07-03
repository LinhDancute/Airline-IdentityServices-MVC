
using Airline.WebClient.Models.Airline;

namespace Airline.WebClient.Models.DTOs.Schedule
{
    public class FlightRouteCreateDTO
    {
        public string DepartureAddress { get; set; }
        public string ArrivalAddress { get; set; }
        public FlightRoute.GateStatusType Gate { get; set; }
        public FlightRoute.FlightRouteStatusType Status { get; set; }
    }
}
