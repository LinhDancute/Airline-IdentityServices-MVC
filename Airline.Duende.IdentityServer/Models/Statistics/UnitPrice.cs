using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Airline.WebClient.Models.Airlines;

namespace Airline.WebClient.Models.Statistics
{

    public class UnitPrice
    {
        [Key]
        public int PriceId { get; set; }
        public decimal USD { get; set; }
        public decimal VND { get; set; }
        public ICollection<Ticket> Tickets { get; } = new List<Ticket>();
    }
}