using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Coupon;
using System.ComponentModel.DataAnnotations;

namespace Airline.ModelsService.Models.DTOs.Schedule
{
    public class BoardingPassDTO
    {
        public int BoardingPassId { get; set; }
        public int TicketId { get; set; }
        public string BoardingTime { get; set; }
        public string BoardingGate { get; set; }
        public string Seat { get; set; }
        public TicketDTO Ticket { get; set; }
    }
}
