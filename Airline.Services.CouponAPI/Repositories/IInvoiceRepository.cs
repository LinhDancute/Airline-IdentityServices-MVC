using Airline.ModelsService.Models.Statistical;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice> GetByIdAsync(int id);
        Task AddAsync(Invoice invoice);
    }
}
