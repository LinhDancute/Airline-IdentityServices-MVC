using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories
{
    public interface IAirlineRepository
    {
        Task<IEnumerable<Airline.Services.ScheduleAPI.Models.Airline>> GetAllAsync();
        Task<Airline.Services.ScheduleAPI.Models.Airline> GetByIdAsync(int id);
        Task AddAsync(Airline.Services.ScheduleAPI.Models.Airline airline); //add single
        Task AddRangeAsync(IEnumerable<Airline.Services.ScheduleAPI.Models.Airline> airlines); //add list
        Task UpdateAsync(Airline.Services.ScheduleAPI.Models.Airline airline);
        Task DeleteAsync(int id);
        Task<bool> AirlineExistsAsync(int id);
        Task<Airline.Services.ScheduleAPI.Models.Airline> FindAsync(Expression<Func<Airline.Services.ScheduleAPI.Models.Airline, bool>> predicate);
        Task<Airline.Services.ScheduleAPI.Models.Airline> FindByIATACodeAsync(string iataCode);
    }
}
