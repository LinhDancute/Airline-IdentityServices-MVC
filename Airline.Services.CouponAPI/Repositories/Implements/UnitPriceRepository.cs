using Airline.ModelsService;
using Airline.ModelsService.Models.Statistical;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class UnitPriceRepository : IUnitPriceRepository
    {
        private readonly AppDbContext _context;

        public UnitPriceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UnitPrice>> GetAllAsync()
        {
            return await _context.Set<UnitPrice>().ToListAsync();
        }

        public async Task<UnitPrice> GetByIdAsync(int id)
        {
            return await _context.Set<UnitPrice>().FindAsync(id);
        }

        public async Task AddAsync(UnitPrice unitPrice)
        {
            await _context.Set<UnitPrice>().AddAsync(unitPrice);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<UnitPrice> unitPrices)
        {
            await _context.Set<UnitPrice>().AddRangeAsync(unitPrices);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UnitPrice unitPrice)
        {
            _context.Set<UnitPrice>().Update(unitPrice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UnitPrice unitPrice)
        {
            _context.Set<UnitPrice>().Remove(unitPrice);
            await _context.SaveChangesAsync();
        }
    }
}
