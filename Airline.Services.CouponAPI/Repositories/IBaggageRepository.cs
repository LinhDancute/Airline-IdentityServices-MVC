using Airline.ModelsService.Models.Airline;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface IBaggageRepository
    {
        Task<IEnumerable<Baggage>> GetAllAsync();
        Task<Baggage> GetByIdAsync(int id);
        Task AddAsync(Baggage baggage);
        Task AddRangeAsync(IEnumerable<Baggage> baggages);
        Task UpdateAsync(Baggage baggage);
        Task DeleteAsync(Baggage baggage);
        Task<List<Baggage>> GetByBaggageNameAsync(List<string> baggageName);
    }
}
