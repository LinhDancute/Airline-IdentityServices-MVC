namespace Airline.Services.ScheduleAPI.Models.DTOs
{
    public class FlightRouteCreateDTO
    {
        public string DepartureAddress { get; set; }
        public string ArrivalAddress { get; set; }
        public FlightRoute.GateStatusType Gate { get; set; }
        public FlightRoute.FlightRouteStatusType Status { get; set; }
    }
}
