using App.Models.Airline;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories
{
    public interface IFlightRoute_FlightRepository
    {
        Task<IEnumerable<FlightRoute_Flight>> GetAllAsync();
        Task AddAsync(FlightRoute_Flight flightRoute_Flight);
        Task AddRangeAsync(IEnumerable<FlightRoute_Flight> flightRoute_Flights);
        Task<FlightRoute_Flight> FindAsync(Expression<Func<FlightRoute_Flight, bool>> predicate);
        Task<IEnumerable<FlightRoute_Flight>> FindAllAsync(Expression<Func<FlightRoute_Flight, bool>> predicate);
        Task DeleteAsync(int flightRouteFlightId);
    }
}
