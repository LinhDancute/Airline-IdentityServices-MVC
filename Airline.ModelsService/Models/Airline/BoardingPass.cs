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
        public TimeSpan? BoardingTime { get; set; }

        [Display(Name = "Cửa lên máy bay")]
        public string BoardingGate { get; set; }

        [Display(Name = "Ghế ngồi")]
        public string Seat { get; set; }
    }
}