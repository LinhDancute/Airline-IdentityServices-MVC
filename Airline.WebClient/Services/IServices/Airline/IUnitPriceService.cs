using Airline.WebClient.Models.DTOs.Coupon;

namespace Airline.WebClient.Services.IServices.Airline
{
    public interface IUnitPriceService
    {
        Task<IEnumerable<UnitPriceDTO>> GetAllAsync();
        Task<UnitPriceDTO> GetByIdAsync(int id);
        Task<UnitPriceDTO> CreateAsync(UnitPriceCreateDTO unitPriceCreateDTO);
        Task CreateBulkAsync(IEnumerable<UnitPriceCreateDTO> unitPriceCreateDTOs);
        Task UpdateAsync(int id, UnitPriceCreateDTO unitPriceCreateDTO);
        Task DeleteAsync(int id);
    }
}
