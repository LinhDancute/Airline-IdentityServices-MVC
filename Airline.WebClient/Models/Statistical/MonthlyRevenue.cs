using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Statistical
{

    public class MonthlyRevenue
    {
        [Key]
        public int MonthlyRevenueId { get; set; }

        // [ForeignKey("AnnualRevenueId")]
        // public AnnualRevenue AnnualRevenueId { get; set; }

        public int? AnnualRevenueId { get; set; }
        public long TicketByMonth { get; set; }
        public decimal Revenue { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}