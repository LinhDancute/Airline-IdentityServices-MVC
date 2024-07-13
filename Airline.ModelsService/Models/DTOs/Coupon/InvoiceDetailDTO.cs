using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.Statistical;

namespace Airline.ModelsService.Models.DTOs.Coupon
{
    public class InvoiceDetailDTO
    {
        public string InvoiceId { get; set; }
        public int TicketId { set; get; }
        public string Class { get; set; }
        public string Itinerary { get; set; }
        public decimal UnitPrice { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public Ticket? Ticket { set; get; }
    }
}
