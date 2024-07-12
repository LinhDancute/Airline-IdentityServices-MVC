
using Airline.ModelsService.Models.Airline;

namespace Airline.ModelsService.Models.DTOs.Schedule
{
    public class FlightRouteCreateDTO
    {
        public string DepartureAddress { get; set; }
        public string ArrivalAddress { get; set; }
        public FlightRoute.GateStatusType Gate { get; set; }
        public FlightRoute.FlightRouteStatusType Status { get; set; }
    }
}
