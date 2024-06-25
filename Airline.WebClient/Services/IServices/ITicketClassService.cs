using Airline.ModelsService.Models.DTOs.Coupon;

namespace Airline.WebClient.Services.IServices
{
    public interface ITicketClassService
    {
        Task<List<TicketClassDTO>> GetAllTicketClassesAsync();
    }
}
