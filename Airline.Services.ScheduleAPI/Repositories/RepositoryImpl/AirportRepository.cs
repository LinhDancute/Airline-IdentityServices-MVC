using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories.RepositoryImpl
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AppDbContext _context;
        public AirportRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<IEnumerable<Airport>> GetAllAsync()
        {
            return await _context.Airports.ToListAsync();
        }

        public async Task<Airport> GetByIdAsync(int id)
        {
            return await _context.Airports.SingleOrDefaultAsync(a => a.AirportId == id);
        }

        //Add signle
        public async Task AddAsync(Airport airport)
        {
            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();
        }

        //Add list
        public async Task AddRangeAsync(IEnumerable<Airport> airports)
        {
            await _context.Airports.AddRangeAsync(airports);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Airport airport)
        {
            _context.Entry(airport).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var airport = await _context.Airports.FindAsync(id);
            if (airport != null)
            {
                _context.Airports.Remove(airport);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AirportExistsAsync(int id)
        {
            return await _context.Airports.AnyAsync(e => e.AirportId == id);
        }

        public async Task<Airport> FindAsync(Expression<Func<Airport, bool>> predicate)
        {
            return await _context.Airports.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<string>> GetSimplifiedAirportNamesAsync()
        {
            var airports = await _context.Airports.ToListAsync();

            var simplifiedNames = airports.Select(a => RemoveSuffix(a.AirportName)).Distinct();

            return simplifiedNames;
        }

        private string RemoveSuffix(string airportName)
        {
            if (airportName.EndsWith(" International Airport", StringComparison.OrdinalIgnoreCase))
            {
                return airportName.Replace(" International Airport", "", StringComparison.OrdinalIgnoreCase).Trim();
            }

            if (airportName.EndsWith(" Airport", StringComparison.OrdinalIgnoreCase))
            {
                return airportName.Replace(" Airport", "", StringComparison.OrdinalIgnoreCase).Trim();
            }

            return airportName;
        }
    }
}
