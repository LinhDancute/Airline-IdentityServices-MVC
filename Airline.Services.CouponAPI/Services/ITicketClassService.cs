<<<<<<< HEAD
﻿using Airline.ModelsService.Models.DTOs.Coupon;
=======
﻿using Airline.WebClient.Models.DTOs.Coupon;
>>>>>>> 015933b5a74e5f2f345a2bfbb51871285fa0aac9

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
