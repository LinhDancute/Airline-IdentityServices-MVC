using Airline.ModelsService.Models.Airline;
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
        Task<IEnumerable<FlightRoute>> GetByIdsAsync(IEnumerable<int> ids);
        Task UpdateAsync(FlightRoute flightRoute);
        Task DeleteAsync(int id);
        Task<bool> FlightRouteExistsAsync(int id);
        Task<IEnumerable<FlightRoute>> GetBySectorAsync(string flightSector);
    }
}
