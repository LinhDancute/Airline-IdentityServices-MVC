using Airline.ModelsService.Models.Statistical;
using System.ComponentModel.DataAnnotations;

namespace Airline.ModelsService.Models.DTOs.Coupon
{
    public class InvoiceDTO
    {
        public string InvoiceId { get; set; }
        public string? PassengerId { set; get; }
        public DateTime Date { get; set; }
        public InvoiceStatusType Status { get; set; }
        public AppUser? Passenger { set; get; }
        public InvoiceDetail InvoiceDetails { get; set; }
    }

    public enum InvoiceStatusType
    {
        Confirmed,
        Cancelled
    }
}
