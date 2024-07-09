using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Coupon;
using Microsoft.EntityFrameworkCore;

namespace Airline.Services.CouponAPI.Repositories.Implements
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Ticket> tickets)
        {
            await _context.Set<Ticket>().AddRangeAsync(tickets);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TicketDTO>> GetAllAsync()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Passenger)
                .Include(t => t.Flight)
                .Include(t => t.UnitPrice)
                .Include(t => t.TicketClass)
                .Include(t => t.Ticket_Baggages).ThenInclude(tb => tb.Baggage)
                .Include(t => t.Ticket_Meals).ThenInclude(tm => tm.Meal)
                .ToListAsync();

            return tickets.Select(ticket => new TicketDTO
            {
                TicketId = ticket.TicketId,
                PassengerId = ticket.Passenger?.Id,
                FlightId = ticket.Flight.FlightId,
                PriceId = ticket.UnitPrice.PriceId,
                ClassId = ticket.TicketClass.TicketId,
                PassengerName = ticket.Passenger?.UserName,
                PassengerPhoneNumber = ticket.Passenger?.PhoneNumber,
                Itinerary = ticket.Flight.FlightSector,
                Date = ticket.Flight.Date,
                DepartureTime = ticket.Flight.DepartureTime,
                Seat = ticket.Seat,
                FlightNumber = ticket.Flight.FlightNumber,
                Class = ticket.TicketClass.TicketName,
                MealRequest = string.Join(", ", ticket.Ticket_Meals.Select(tm => tm.Meal.MealCode)),
                BaggageType = string.Join(", ", ticket.Ticket_Baggages.Select(tb => tb.Baggage.Name)),
                VND = ticket.UnitPrice.VND.ToString(),
                USD = ticket.UnitPrice.USD.ToString(),
                PNR = ticket.PNR,
                Status = (TicketStatus)ticket.Status,
                StatusName = ((TicketStatus)ticket.Status).ToString(),
                Passenger = ticket.Passenger,
                Flight = ticket.Flight,
                UnitPrice = ticket.UnitPrice,
                TicketClass = ticket.TicketClass,
                Ticket_Baggages = ticket.Ticket_Baggages,
                Ticket_Meals = ticket.Ticket_Meals,
            }).ToList();
        }


        private TicketStatus MapTicketStatus(TicketStatusType status)
        {
            return status switch
            {
                TicketStatusType.Confirmed => TicketStatus.Confirmed,
                TicketStatusType.Pending => TicketStatus.Pending,
                TicketStatusType.Refundable => TicketStatus.Refundable,
                TicketStatusType.Nonrefundable => TicketStatus.Nonrefundable,
                TicketStatusType.Cancelled => TicketStatus.Cancelled,
                _ => throw new ArgumentOutOfRangeException(nameof(status), $"Not expected status value: {status}")
            };
        }


        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Passenger)
                .Include(t => t.Flight)
                .Include(t => t.UnitPrice)
                .Include(t => t.TicketClass)
                .Include(t => t.Ticket_Baggages).ThenInclude(tb => tb.Baggage)
                .Include(t => t.Ticket_Meals).ThenInclude(tm => tm.Meal)
                .FirstOrDefaultAsync(t => t.TicketId == id);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task AddTicket_BaggageAsync(Ticket_Baggage ticket_Baggage)
        {
            _context.Ticket_Baggages.Add(ticket_Baggage);
            await _context.SaveChangesAsync();
        }

        public async Task AddTicket_MealAsync(Ticket_Meal ticket_Meal)
        {
            _context.Ticket_Meals.Add(ticket_Meal);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLatestTicketIdAsync()
        {
            var latestTicket = await _context.Tickets.OrderByDescending(t => t.TicketId).FirstOrDefaultAsync();
            return latestTicket != null ? latestTicket.TicketId : 0;
        }

        public async Task<int> GenerateNextTicketIdAsync()
        {
            int latestId = await GetLatestTicketIdAsync();
            return latestId + 1;
        }

    }
}
