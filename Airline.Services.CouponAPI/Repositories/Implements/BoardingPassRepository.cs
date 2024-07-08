using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class BoardingPassRepository : IBoardingPassRepository
    {
        private readonly AppDbContext _context;

        public BoardingPassRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BoardingPass>> GetAllAsync()
        {
            return await _context.BoardingPasses
                                .Include(bp => bp.Ticket)  
                                .ThenInclude(t => t.Passenger)  
                                .ToListAsync();
        }

        public async Task<BoardingPass> GetByIdAsync(int id)
        {
            return await _context.Set<BoardingPass>().FindAsync(id);
        }

        public async Task AddAsync(BoardingPass boardingPass)
        {
            await _context.Set<BoardingPass>().AddAsync(boardingPass);
            await _context.SaveChangesAsync();
        }
    }
}
