using Airline.ModelsService.Models.Statistical;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface IInvoiceDetailRepository
    {
        Task<IEnumerable<InvoiceDetail>> GetAllAsync();
        Task<InvoiceDetail> GetByIdAsync(int id);
        Task AddAsync(InvoiceDetail invoiceDetail);
    }
}
