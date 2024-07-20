using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class TicketClassRepository : ITicketClassRepository
    {
        private readonly AppDbContext _context;

        public TicketClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketClass>> GetAllAsync()
        {
            return await _context.Set<TicketClass>().ToListAsync();
        }

        public async Task<TicketClass> GetByIdAsync(int id)
        {
            return await _context.Set<TicketClass>().FindAsync(id);
        }

        public async Task AddAsync(TicketClass ticketClass)
        {
            await _context.Set<TicketClass>().AddAsync(ticketClass);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TicketClass> ticketClasses)
        {
            await _context.Set<TicketClass>().AddRangeAsync(ticketClasses);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TicketClass ticketClass)
        {
            _context.Set<TicketClass>().Update(ticketClass);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TicketClass ticketClass)
        {
            _context.Set<TicketClass>().Remove(ticketClass);
            await _context.SaveChangesAsync();
        }

        //signle
        public async Task<TicketClass> GetTicketClassByNameAsync(string className)
        {
            return await _context.TicketClasses
                .FirstOrDefaultAsync(tc => tc.TicketName == className);
        }

        //list
        public async Task<List<TicketClass>> GetTicketClassesByNameAsync(string className)
        {
            return await _context.TicketClasses
                .Where(tc => tc.TicketName.Contains(className))
                .ToListAsync();
        }
    }
}
