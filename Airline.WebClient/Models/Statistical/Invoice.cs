using Airline.ModelsService.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.WebClient.Models.Statistical
{
    public class Invoice
    {
        [Key]
        public string InvoiceId { get; set; }
        public string? PassengerId { set; get; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public InvoiceStatus Status { get; set; }
        public AppUser? Passenger { set; get; }
        public InvoiceDetail InvoiceDetails { get; set; }
    }

    public enum InvoiceStatus
    {
        Confirmed,
        Cancelled
    }
}
