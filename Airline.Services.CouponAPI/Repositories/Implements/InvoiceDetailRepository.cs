using Airline.ModelsService;
using Airline.ModelsService.Models.Statistical;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class InvoiceDetailRepository : IInvoiceDetailRepository
    {
        private readonly AppDbContext _context;

        public InvoiceDetailRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<InvoiceDetail>> GetAllAsync()
        {
            return await _context.InvoiceDetails
                                .Include(bp => bp.Ticket)
                                .ToListAsync();
        }

        public async Task<InvoiceDetail> GetByIdAsync(int id)
        {
            return await _context.Set<InvoiceDetail>().FindAsync(id);
        }

        public async Task AddAsync(InvoiceDetail invoiceDetail)
        {
            await _context.Set<InvoiceDetail>().AddAsync(invoiceDetail);
            await _context.SaveChangesAsync();
        }
    }
}
