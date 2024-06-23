using Airline.ModelsService.Models.DTOs.Coupon;

namespace Airline.Services.CouponAPI.Services
{
    public interface ITicketClassService
    {
        Task<IEnumerable<TicketClassDTO>> GetAllAsync();
        Task<TicketClassDTO> GetByIdAsync(int id);
        Task CreateAsync(TicketClassCreateDTO ticketClassDTO);
        Task CreateBulkAsync(IEnumerable<TicketClassCreateDTO> ticketClassDTOs);
        Task UpdateAsync(int id, TicketClassCreateDTO ticketClassDTO);
        Task DeleteAsync(int id);
    }
}
