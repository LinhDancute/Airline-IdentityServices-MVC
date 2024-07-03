using Airline.WebClient.Models.DTOs.Coupon;

namespace Airline.WebClient.Services.IServices.Airline
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
