using Airline.WebClient.Models.DTOs.Schedule;

namespace Airline.Services.ScheduleAPI.Services
{
    public interface IFlightRoute_AirportService
    {
        Task<IEnumerable<FlightRoute_AirportDTO>> GetAllFlightRoutes_AirportsAsync();
        Task CreateFlightRoutes_AirportsAsync(FlightRoute_AirportDTO flightRoute_AirportDTO); //add single
        Task CreateFlightRoutes_AirportsAsync(List<FlightRoute_AirportDTO> flightRoute_AirportDTOs); //add list
    }
}
