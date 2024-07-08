using Airline.ModelsService.Models.DTOs.Coupon;

namespace Airline.Services.CouponAPI.Services
{
    public interface ITicketService
    {
        Task CreateAsync(TicketCreateDTO ticketDTO);
        Task CreateBulkAsync(IEnumerable<TicketCreateDTO> ticketDTOs, string userId);
        Task<IEnumerable<TicketDTO>> GetAllAsync();
        Task<TicketDTO> GetByIdAsync(int id);
        Task UpdateAsync(int id, TicketCreateDTO ticketDTO);
        Task DeleteAsync(int id);
    }
}
