using Airline.WebClient.Models.DTOs.Coupon;

namespace Airline.WebClient.Services.IServices.Airline
{
    public interface IMealService
    {
        Task<IEnumerable<MealDTO>> GetAllAsync();
        Task<MealDTO> GetByIdAsync(int id);
        Task CreateAsync(MealCreateDTO mealDTO);
        Task CreateBulkAsync(IEnumerable<MealCreateDTO> mealDTOs);
        Task UpdateAsync(int id, MealCreateDTO mealDTO);
        Task DeleteAsync(int id);
    }
}
