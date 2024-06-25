using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories.RepositoryImpl
{
    public class FlightRouteRepository : IFlightRouteRepository
    {
        private readonly AppDbContext _context;

        public FlightRouteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FlightRoute>> GetAllAsync()
        {
            return await _context.FlightRoutes.ToListAsync();
        }

        public async Task<FlightRoute> GetByIdAsync(int id)
        {
            return await _context.FlightRoutes.SingleOrDefaultAsync(a => a.FlightRouteId == id);
        }

        public async Task<IEnumerable<FlightRoute>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.FlightRoutes.Where(fr => ids.Contains(fr.FlightRouteId)).ToListAsync();
        }

        //Add signle
        public async Task AddAsync(FlightRoute flightRoute)
        {
            await _context.FlightRoutes.AddAsync(flightRoute);
            await _context.SaveChangesAsync();
        }

        //Add list
        public async Task AddRangeAsync(IEnumerable<FlightRoute> flightRoutes)
        {
            await _context.FlightRoutes.AddRangeAsync(flightRoutes);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(FlightRoute flightRoute)
        {
            _context.Entry(flightRoute).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var flightRoute = await _context.FlightRoutes.FindAsync(id);
            if (flightRoute != null)
            {
                _context.FlightRoutes.Remove(flightRoute);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> FlightRouteExistsAsync(int id)
        {
            return await _context.FlightRoutes.AnyAsync(e => e.FlightRouteId == id);
        }

        public async Task<FlightRoute> FindAsync(Expression<Func<FlightRoute, bool>> predicate)
        {
            return await _context.FlightRoutes.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<FlightRoute>> GetBySectorAsync(string flightSector)
        {
            return await _context.FlightRoutes.Where(fr => fr.FlightSector == flightSector).ToListAsync();
        }
    }
}
