using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class BaggageRepository : IBaggageRepository
    {
        private readonly AppDbContext _context;

        public BaggageRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Baggage>> GetAllAsync()
        {
            return await _context.Set<Baggage>().ToListAsync();
        }

        public async Task<Baggage> GetByIdAsync(int id)
        {
            return await _context.Set<Baggage>().FindAsync(id);
        }

        public async Task AddAsync(Baggage baggage)
        {
            await _context.Set<Baggage>().AddAsync(baggage);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Baggage> baggages)
        {
            await _context.Set<Baggage>().AddRangeAsync(baggages);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Baggage baggage)
        {
            _context.Set<Baggage>().Update(baggage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Baggage baggage)
        {
            _context.Set<Baggage>().Remove(baggage);
            await _context.SaveChangesAsync();
        }
    }
}
