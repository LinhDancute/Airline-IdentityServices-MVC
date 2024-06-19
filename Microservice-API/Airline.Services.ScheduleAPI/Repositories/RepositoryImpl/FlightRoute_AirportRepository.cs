using Airline.Services.ScheduleAPI.Data;
using Airline.Services.ScheduleAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories.RepositoryImpl
{
    public class FlightRoute_AirportRepository : IFlightRoute_AirportRepository
    {
        private readonly AppDbContext _context;

        public FlightRoute_AirportRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FlightRoute_Airport>> GetAllAsync()
        {
            return await _context.FlightsRoute_Airports.ToListAsync();
        }
        public async Task AddAsync(FlightRoute_Airport flightRoute_Airport)
        {
            await _context.FlightsRoute_Airports.AddAsync(flightRoute_Airport);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<FlightRoute_Airport> flightRoute_Airports)
        {
            await _context.FlightsRoute_Airports.AddRangeAsync(flightRoute_Airports);
            await _context.SaveChangesAsync();
        }
        public async Task<FlightRoute_Airport> FindAsync(Expression<Func<FlightRoute_Airport, bool>> predicate)
        {
            return await _context.FlightsRoute_Airports.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<FlightRoute_Airport>> FindAllAsync(Expression<Func<FlightRoute_Airport, bool>> predicate)
        {
            return await _context.FlightsRoute_Airports.Where(predicate).ToListAsync();
        }

        public async Task DeleteAsync(FlightRoute_Airport flightRoute_Airport)
        {
            _context.FlightsRoute_Airports.Remove(flightRoute_Airport);
            await _context.SaveChangesAsync();
        }
    }
}
