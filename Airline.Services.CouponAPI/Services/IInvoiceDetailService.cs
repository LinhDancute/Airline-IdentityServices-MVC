using Airline.ModelsService.Models.DTOs.Coupon;

namespace Airline.Services.CouponAPI.Services
{
    public interface IInvoiceDetailService
    {
        Task<IEnumerable<InvoiceDetailDTO>> GetAllAsync();
        Task<InvoiceDetailDTO> GetByIdAsync(int id);
        Task CreateAsync(InvoiceDetailDTO invoiceDetailDTO);
    }
}
