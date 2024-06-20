using App.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories.RepositoryImpl
{
    public class AirlineRepository : IAirlineRepository
    {
        private readonly AppDbContext _context;
        public AirlineRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<IEnumerable<App.Models.Airline.Airline>> GetAllAsync()
        {
            return await _context.Airlines.Include(a => a.AirlineChildren).ToListAsync();
        }

        public async Task<App.Models.Airline.Airline> GetByIdAsync(int id)
        {
            return await _context.Airlines.Include(a => a.AirlineChildren)
                                          .SingleOrDefaultAsync(a => a.AirlineId == id);
        }

        //Add signle
        public async Task AddAsync(App.Models.Airline.Airline airline)
        {
            _context.Airlines.Add(airline);
            await _context.SaveChangesAsync();
        }

        //Add list
        public async Task AddRangeAsync(IEnumerable<App.Models.Airline.Airline> airlines) 
        {
            await _context.Airlines.AddRangeAsync(airlines);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(App.Models.Airline.Airline airline)
        {
            _context.Entry(airline).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var airline = await _context.Airlines.FindAsync(id);
            if (airline != null)
            {
                _context.Airlines.Remove(airline);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AirlineExistsAsync(int id)
        {
            return await _context.Airlines.AnyAsync(e => e.AirlineId == id);
        }

        public async Task<App.Models.Airline.Airline> FindAsync(Expression<Func<App.Models.Airline.Airline, bool>> predicate)
        {
            return await _context.Airlines.FirstOrDefaultAsync(predicate);
        }

        public async Task<App.Models.Airline.Airline> FindByIATACodeAsync(string iataCode)
        {
            return await _context.Airlines.FirstOrDefaultAsync(a => a.IATAcode == iataCode);
        }
    }
}
