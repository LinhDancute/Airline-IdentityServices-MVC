namespace Airline.ModelsService.Models.DTOs.Coupon
{
    public class TicketClassDTO
    {
        public int TicketClassId { get; set; }
        public string TicketName { get; set; }
        public string FareClass { get; set; }
        public string? Description { get; set; }
    }
}
