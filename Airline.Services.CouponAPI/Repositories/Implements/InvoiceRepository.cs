using Airline.ModelsService;
using Airline.ModelsService.Models.Statistical;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                                .Include(bp => bp.Passenger)
                                .ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Set<Invoice>().FindAsync(id);
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _context.Set<Invoice>().AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
