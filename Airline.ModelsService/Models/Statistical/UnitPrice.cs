using Airline.ModelsService.Models.Airline;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.ModelsService.Models.Statistical
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