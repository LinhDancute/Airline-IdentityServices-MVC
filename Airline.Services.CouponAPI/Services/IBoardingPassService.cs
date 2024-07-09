using Airline.ModelsService.Models.DTOs.Schedule;

namespace Airline.Services.CouponAPI.Services
{
    public interface IBoardingPassService
    {
        Task<IEnumerable<BoardingPassDTO>> GetAllAsync();
        Task<BoardingPassDTO> GetByIdAsync(int id);
        Task CreateAsync(BoardingPassDTO boardingPassDTO);
    }
}
