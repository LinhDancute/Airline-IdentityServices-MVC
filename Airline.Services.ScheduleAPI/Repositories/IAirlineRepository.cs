using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories
{
    public interface IAirlineRepository
    {
        Task<IEnumerable<ModelsService.Models.Airline.Airline>> GetAllAsync();
        Task<ModelsService.Models.Airline.Airline> GetByIdAsync(int id);
        Task AddAsync(ModelsService.Models.Airline.Airline airline); //add single
        Task AddRangeAsync(IEnumerable<ModelsService.Models.Airline.Airline> airlines); //add list
        Task UpdateAsync(ModelsService.Models.Airline.Airline airline);
        Task DeleteAsync(int id);
        Task<bool> AirlineExistsAsync(int id);
        Task<ModelsService.Models.Airline.Airline> FindAsync(Expression<Func<ModelsService.Models.Airline.Airline, bool>> predicate);
        Task<ModelsService.Models.Airline.Airline> FindByIATACodeAsync(string iataCode);
    }
}
