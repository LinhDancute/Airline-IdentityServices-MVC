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
        public int FlightId { get; set; }
        public string? PassengerId { set; get; }
        public int ClassId { get; set; }
        public int TicketId { get; set; }
        public int BaggageId { get; set; }
        public int MealId { get; set; }
        public string PNR { get; set; }

        [Display(Name = "Mã vé điện tử")]
        public string ETicket { set; get; }

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

        [Display(Name = "Hạng vé")]
        public string Class { get; set; }

        [Display(Name = "Hành lý")]
        public string BaggageType { get; set; }

        [Display(Name = "Yêu cầu dịch vụ đặc biệt")]
        public string SSR { get; set; }

        [Display(Name = "Chặng bay")]
        public string Itinerary { get; set; }
        public AppUser? Passenger { set; get; }
        public Flight Flight { get; set; }
        public TicketClass TicketClass { get; set; }
        public Baggage Baggage { get; set; }
        public Meal Meal { get; set; }
        public Ticket Ticket { get; set; }
    }
}