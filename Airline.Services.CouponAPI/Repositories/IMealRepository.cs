using Airline.ModelsService.Models.Airline;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface IMealRepository
    {
        Task<IEnumerable<Meal>> GetAllAsync();
        Task<Meal> GetByIdAsync(int id);
        Task AddAsync(Meal meal);
        Task AddRangeAsync(IEnumerable<Meal> meals);
        Task UpdateAsync(Meal meal);
        Task DeleteAsync(Meal meal);
        Task<Meal> GetMealByCodeAsync(string mealCode);
        Task<List<Meal>> GetMealByCodesAsync(List<string> mealCodes);
    }
}
