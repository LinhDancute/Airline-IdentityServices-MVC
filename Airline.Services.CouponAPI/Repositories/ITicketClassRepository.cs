<<<<<<< HEAD
﻿
using Airline.ModelsService.Models.Airline;
=======
﻿using App.Models.Airline;
>>>>>>> 015933b5a74e5f2f345a2bfbb51871285fa0aac9

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
