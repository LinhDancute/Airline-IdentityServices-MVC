using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories
{
    public interface IAirlineRepository
    {
        Task<IEnumerable<App.Models.Airline.Airline>> GetAllAsync();
        Task<App.Models.Airline.Airline> GetByIdAsync(int id);
        Task AddAsync(App.Models.Airline.Airline airline); //add single
        Task AddRangeAsync(IEnumerable<App.Models.Airline.Airline> airlines); //add list
        Task UpdateAsync(App.Models.Airline.Airline airline);
        Task DeleteAsync(int id);
        Task<bool> AirlineExistsAsync(int id);
        Task<App.Models.Airline.Airline> FindAsync(Expression<Func<App.Models.Airline.Airline, bool>> predicate);
        Task<App.Models.Airline.Airline> FindByIATACodeAsync(string iataCode);
    }
}
