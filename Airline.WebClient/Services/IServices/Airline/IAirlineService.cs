using Airline.WebClient.Models.DTOs.Schedule;

namespace Airline.WebClient.Services.IServices.Airline
{
    public interface IAirlineService
    {
        Task<IEnumerable<AirlineDTO>> GetAllAirlinesAsync();
        Task<AirlineDTO> GetAirlineByIdAsync(int id);
        Task CreateAirlineAsync(AirlineDTO airlineDto); //add signle
        Task CreateAirlinesAsync(List<AirlineDTO> airlineDTOs); //add list
        Task UpdateAirlineAsync(int id, AirlineDTO airlineDto);
        Task DeleteAirlineAsync(int id);
        Task<bool> AirlineExistsAsync(int id);
    }
}
