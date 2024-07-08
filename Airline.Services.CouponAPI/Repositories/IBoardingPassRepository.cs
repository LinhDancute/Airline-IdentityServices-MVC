using Airline.ModelsService.Models.Airline;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface IBoardingPassRepository
    {
        Task<IEnumerable<BoardingPass>> GetAllAsync();
        Task<BoardingPass> GetByIdAsync(int id);
        Task AddAsync(BoardingPass boardingPass);
    }
}
