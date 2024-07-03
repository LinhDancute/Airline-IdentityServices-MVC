using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Schedule;

namespace Airline.Services.ScheduleAPI.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDTO>> GetAllFlightsAsync();
        Task CreateFlightAsync(FlightCreateDTO flightDTO); //add signle
        Task CreateFlightsAsync(List<FlightCreateDTO> flightDTOs); //add list
        Task UpdateFlightAsync(int flightId, FlightCreateDTO flightDTO);
        Task CloseFlightAsync(int flightId); //close flight, Staus Active -> Closed
        Task DeleteFlightAsync(int flightId);
        Task<Flight> GetFlightByIdAsync(int id);
    }
}
