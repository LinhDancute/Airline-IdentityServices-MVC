namespace Airline.WebClient.Models.DTOs.Coupon
{
    public class TicketClassDTO
    {
        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public string FareClass { get; set; }
        public string? Description { get; set; }
    }
}
