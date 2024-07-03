using Airline.ModelsService.Models.DTOs.Coupon;

namespace Airline.Services.CouponAPI.Services
{
    public interface IBaggageService
    {
        Task<IEnumerable<BaggageDTO>> GetAllAsync();
        Task<BaggageDTO> GetByIdAsync(int id);
        Task CreateAsync(BaggageCreateDTO baggageCreateDTO);
        Task CreateBulkAsync(IEnumerable<BaggageCreateDTO> baggageCreateDTOs);
        Task UpdateAsync(int id, BaggageCreateDTO baggageCreateDTO);
        Task DeleteAsync(int id);
    }
}
