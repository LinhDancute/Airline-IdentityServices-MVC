
using Airline.ModelsService.Models.DTOs.Schedule;

namespace Airline.Services.ScheduleAPI.Services
{
    public interface IAirportService
    {
        Task<IEnumerable<AirportDTO>> GetAllAirportsAsync();
        Task<AirportDTO> GetAirportByIdAsync(int id);
        Task CreateAirportAsync(AirportCreateDTO airportDTO); //add signle
        Task CreateAirportsAsync(List<AirportCreateDTO> airportDTOs); //add list
        Task UpdateAirportAsync(int id, AirportCreateDTO airportDTO);
        Task CloseAirportAsync(int id, AirportDTO airportDTO); //close airport, Staus Active -> Closed
        Task DeleteAirportAsync(int id);
        Task<bool> AirportExistsAsync(int id);
        Task<AirportDTO> GetAirportByNameAsync(string airportName);
    }
}
