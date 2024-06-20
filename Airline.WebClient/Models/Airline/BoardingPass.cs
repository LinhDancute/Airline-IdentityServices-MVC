using Bogus.DataSets;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Airline
{
    public class BoardingPass
    {
        [Key]
        public int BoardingPassId { get; set; }
        public int FlightId { get; set; }
        public string? PassengerId { set; get; }

        [Display(Name = "Ngày bay")]
        public DateTime Date { get; set; }
        [Display(Name = "Thời gian bay")]
        public TimeSpan DepartureTime { get; set; }
        [Display(Name = "Ghế đặt chỗ")]
        public string Seat { get; set; }
        [Display(Name = "Cổng")]
        public string Gate { get; set; }
        [Display(Name = "Giờ lên máy bay")]
        public TimeSpan BoardingTime { get; set; }
        public string FlightNumber { get; set; }

        [Display(Name = "Tên hành khách")]
        public string? PassengerName { get; set; }
        public AppUser? Passenger { set; get; }
        public Flight Flight { get; set; }
        public ICollection<BoardingPass_TicketClass>? BoardingPass_TicketClasses { get; set; }
    }
}