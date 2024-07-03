using Airline.ModelsService.Models;
using Airline.ModelsService.Models.Airline;
using Bogus.DataSets;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.ModelsService.Models.Airline
{
    public class BoardingPass
    {
        [Key]
        public int BoardingPassId { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        [Display(Name = "Boarding Time")]
        public DateTime BoardingTime { get; set; }

        [Display(Name = "Gate")]
        public string Gate { get; set; }

        [Display(Name = "Seat")]
        public string Seat { get; set; }
    }
}