using Airline.Services.ScheduleAPI.Models;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories
{
    public interface IFlightRouteRepository
    {
        Task<IEnumerable<FlightRoute>> GetAllAsync();
        Task<FlightRoute> FindAsync(Expression<Func<FlightRoute, bool>> predicate);
        Task AddAsync(FlightRoute flightRoute); //add single
        Task AddRangeAsync(IEnumerable<FlightRoute> flightRoutes); //add list
        Task<FlightRoute> GetByIdAsync(int id);
        
        Task UpdateAsync(FlightRoute flightRoute);
        Task DeleteAsync(int id);
        Task<bool> FlightRouteExistsAsync(int id);
    }
}
