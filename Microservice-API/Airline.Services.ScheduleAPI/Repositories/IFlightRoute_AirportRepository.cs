using App.Models.Airline;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories
{
    public interface IFlightRoute_AirportRepository
    {
        Task<IEnumerable<FlightRoute_Airport>> GetAllAsync();
        Task AddAsync(FlightRoute_Airport flightRoute_Airport);
        Task AddRangeAsync(IEnumerable<FlightRoute_Airport> flightRoute_Airports);
        Task<FlightRoute_Airport> FindAsync(Expression<Func<FlightRoute_Airport, bool>> predicate);
        Task<IEnumerable<FlightRoute_Airport>> FindAllAsync(Expression<Func<FlightRoute_Airport, bool>> predicate);
        Task DeleteAsync(FlightRoute_Airport flightRoute_Airport);
    }
}
