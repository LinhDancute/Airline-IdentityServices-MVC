using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.ModelsService.Models.Statistical
{

    public class AnnualRevenue
    {
        [Key]
        public int AnnualRevenueId { get; set; }
        public long TicketByYear { get; set; }
        public decimal Revenue { get; set; }
    }
}