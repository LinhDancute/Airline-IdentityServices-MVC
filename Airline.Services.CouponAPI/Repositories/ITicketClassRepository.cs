using Airline.ModelsService.Models.Airline;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface ITicketClassRepository
    {
        Task<IEnumerable<TicketClass>> GetAllAsync();
        Task<TicketClass> GetByIdAsync(int id);
        Task AddAsync(TicketClass ticketClass);
        Task AddRangeAsync(IEnumerable<TicketClass> ticketClasses);
        Task UpdateAsync(TicketClass ticketClass);
        Task DeleteAsync(TicketClass ticketClass);
    }
}
