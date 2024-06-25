using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Schedule;

namespace Airline.Services.ScheduleAPI.Services
{
    public interface IFlightRouteService
    {
        Task<IEnumerable<FlightRouteDTO>> GetAllFlightRoutesAsync();
        Task CreateFlightRouteAsync(FlightRouteCreateDTO flightRouteDTO); //add signle
        Task CreateFlightRoutesAsync(List<FlightRouteCreateDTO> flightRouteDTOs); //add list
        Task UpdateFlightRouteAsync(int id, FlightRouteCreateDTO flightRouteDTO);
        Task CloseFlightRouteAsync(int id, FlightRouteCreateDTO flightRouteDTO); //close airport, Staus Active -> Closed
        Task DeleteFlightRouteAsync(int id);
        Task<bool> FlightRouteExistsAsync(int id);
        Task<Airport> GetAirportByAbbreviationAsync(string abbreviation); //Abbreviation - Airport (FlightRoute_Airport - AirportID)
        Task<FlightRoute> GetFlightRouteByIdAsync(int id);
    }
}
