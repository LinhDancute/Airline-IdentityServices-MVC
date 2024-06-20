using App.Models.Airline;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight> FindAsync(Expression<Func<Flight, bool>> predicate);
        Task AddAsync(Flight flight); //add single
        Task AddRangeAsync(IEnumerable<Flight> flights); //add list
        Task<Flight> GetByIdAsync(int id);
        Task UpdateAsync(Flight flight);
        Task DeleteAsync(int id);
        Task<bool> FlightExistsAsync(int id);
    }
}
