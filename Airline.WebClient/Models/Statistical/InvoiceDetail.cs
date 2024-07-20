﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Airline.WebClient.Models.Airline;

namespace Airline.WebClient.Models.Statistical
{
    public class InvoiceDetail
    {
        public string InvoiceId { get; set; }
        public int TicketId { set; get; }
        public string Class { get; set; }
        public string Itinerary { get; set; }
        public string UnitPrice { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public Ticket? Ticket { set; get; } 
    }
}
