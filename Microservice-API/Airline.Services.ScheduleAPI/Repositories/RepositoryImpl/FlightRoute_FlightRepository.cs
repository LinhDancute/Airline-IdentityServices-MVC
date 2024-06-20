using App.Models;
using App.Models.Airline;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories.RepositoryImpl
{
    public class FlightRoute_FlightRepository : IFlightRoute_FlightRepository
    {
        private readonly AppDbContext _context;

        public FlightRoute_FlightRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FlightRoute_Flight>> GetAllAsync()
        {
            return await _context.FlightRoute_Flights.ToListAsync();
        }
        public async Task AddAsync(FlightRoute_Flight flightRoute_Flight)
        {
            await _context.FlightRoute_Flights.AddAsync(flightRoute_Flight);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<FlightRoute_Flight> flightRoute_Flights)
        {
            await _context.FlightRoute_Flights.AddRangeAsync(flightRoute_Flights);
            await _context.SaveChangesAsync();
        }
        public async Task<FlightRoute_Flight> FindAsync(Expression<Func<FlightRoute_Flight, bool>> predicate)
        {
            return await _context.FlightRoute_Flights.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<FlightRoute_Flight>> FindAllAsync(Expression<Func<FlightRoute_Flight, bool>> predicate)
        {
            return await _context.FlightRoute_Flights.Where(predicate).ToListAsync();
        }

        public async Task DeleteAsync(int flightRouteFlightId)
        {
            var flightRouteFlight = await _context.FlightRoute_Flights.FindAsync(flightRouteFlightId);
            if (flightRouteFlight != null)
            {
                _context.FlightRoute_Flights.Remove(flightRouteFlight);
                await _context.SaveChangesAsync();
            }
        }

    }
}
