using Airline.ModelsService.Models.Statistical;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface IUnitPriceRepository
    {
        Task<IEnumerable<UnitPrice>> GetAllAsync();
        Task<UnitPrice> GetByIdAsync(int id);
        Task AddAsync(UnitPrice unitPrice);
        Task AddRangeAsync(IEnumerable<UnitPrice> unitPrices);
        Task UpdateAsync(UnitPrice unitPrice);
        Task DeleteAsync(UnitPrice unitPrice);
    }
}
