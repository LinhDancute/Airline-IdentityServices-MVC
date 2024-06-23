using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.Statistical;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;

namespace Airline.ModelsService.Models
{
    public class AppUser : IdentityUser {
        [Column(TypeName ="nvarchar")]
        [StringLength(400)]
        public string? HomeAddress { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(12)]
        [Display(Name = "CMND")]
        public string? CMND { get; set; }

        public ICollection<BoardingPass> BoardingPasses { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}