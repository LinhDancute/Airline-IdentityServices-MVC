using Airline.ModelsService.Models.DTOs.Coupon;

namespace Airline.Services.CouponAPI.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDTO>> GetAllAsync();
        Task<InvoiceDTO> GetByIdAsync(int id);
        Task CreateAsync(InvoiceDTO invoiceDTO);
    }
}
