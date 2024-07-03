using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class MealRepository : IMealRepository
    {
        private readonly AppDbContext _context;

        public MealRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Meal>> GetAllAsync()
        {
            return await _context.Set<Meal>().ToListAsync();
        }

        public async Task<Meal> GetByIdAsync(int id)
        {
            return await _context.Set<Meal>().FindAsync(id);
        }

        public async Task AddAsync(Meal meal)
        {
            await _context.Set<Meal>().AddAsync(meal);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Meal> meals)
        {
            await _context.Set<Meal>().AddRangeAsync(meals);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meal meal)
        {
            _context.Set<Meal>().Update(meal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Meal meal)
        {
            _context.Set<Meal>().Remove(meal);
            await _context.SaveChangesAsync();
        }
    }
}
