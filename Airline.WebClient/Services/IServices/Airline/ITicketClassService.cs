using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Models.DTOs.Schedule;

namespace Airline.WebClient.Services.IServices.Airline
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
