namespace Airline.ModelsService.Models.DTOs.Coupon
{
    public class TicketClassCreateDTO
    {
        public string TicketName { get; set; }
        public string FareClass { get; set; }
        public string? Description { get; set; }
    }
}
