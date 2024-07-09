using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Schedule;
using System.Linq.Expressions;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Flight>> GetAllFlightAsync();
        Task UpdateFlightAsync(Flight flight);
        Task<Flight> FindFlightAsync(Expression<Func<Flight, bool>> predicate);
        Task<FlightRoute> GetByFlightSectorAsync(string flightSector);
    }
}
