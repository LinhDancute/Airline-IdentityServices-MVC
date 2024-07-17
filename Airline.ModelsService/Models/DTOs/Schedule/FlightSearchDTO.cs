namespace Airline.ModelsService.Models.DTOs.Schedule
{
    public class FlightSearchDTO
    {
        public string DepartureAddress { get; set; }
        public string ArrivalAddress { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
