using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Coupon;

namespace Airline.Services.CouponAPI.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<TicketDTO>> GetAllAsync();
        Task<Ticket> GetByIdAsync(int id);
        Task AddAsync(Ticket ticket);
        Task AddRangeAsync(IEnumerable<Ticket> tickets);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(Ticket ticket);
        Task AddTicket_BaggageAsync(Ticket_Baggage ticket_Baggage);
        Task AddTicket_MealAsync(Ticket_Meal ticket_Meal);
        Task<int> GetLatestTicketIdAsync();
        Task<int> GenerateNextTicketIdAsync();
    }
}
