using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> FindFlightAsync(Expression<Func<Flight, bool>> predicate)
        {
            return await _context.Flights.FirstOrDefaultAsync(predicate);
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            _context.Entry(flight).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<FlightRoute> GetByFlightSectorAsync(string flightSector)
        {
            return await _context.FlightRoutes
                .SingleOrDefaultAsync(fr => fr.FlightSector == flightSector);
        }
    }
}
