using App.Models.Airline;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories
{
    public interface IAirportRepository
    {
        Task<IEnumerable<Airport>> GetAllAsync();
        Task<Airport> GetByIdAsync(int id);
        Task AddAsync(Airport airport); //add single
        Task AddRangeAsync(IEnumerable<Airport> airports); //add list
        Task UpdateAsync(Airport airport);
        Task DeleteAsync(int id);
        Task<bool> AirportExistsAsync(int id);
        Task<Airport> FindAsync(Expression<Func<Airport, bool>> predicate);
    }
}
