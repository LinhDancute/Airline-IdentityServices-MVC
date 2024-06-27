using Airline.ModelsService.Models;
using Airline.WebClient.Models.Statistical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Airline.WebClient.Models.Airline
{
    public class Ticket{
        [Key]
        public int TicketId { get; set; }
        public string? PassengerId { set; get; }
        public int FlightId { get; set; }
        public int PriceId { get; set; }
        public int MealId { get; set; }
        public int BaggageId { get; set; }
        public int ClassId { get; set; }

        [Display(Name = "Phát hành")]
        public bool Published { get; set; }
        [Display(Name = "Tên hành khách")]
        public string PassengerName { get; set; }
        [Display(Name = "Chặng bay")]
        public string Itinerary { get; set; }
        [Display(Name = "Số hiệu máy bay")]
        public string FlightNumber { get; set; }
        [Display(Name = "Ngày bay")]
        public DateTime Date { get; set; }
        [Display(Name = "Thời gian bay")]
        public TimeSpan DepartureTime { get; set; }
        [Display(Name = "Ghế đặt chỗ")]
        public string Seat { get; set; }
        [Display(Name = "Hạng vé")]
        public string Class { get; set; }
        public string PNR { get; set; }
        public string MealRequest { get; set; }
        public TicketStatusType Status { get; set; }
        public AppUser? Passenger { set; get; }
        public Flight Flight { get; set; }
        public UnitPrice UnitPrice { get; set; }
        public TicketClass TicketClass { get; set; }
        public Meal Meal { get; set; }
        public Baggage Baggage { get; set; }
        public ICollection<BoardingPass>? BoardingPasses { get; } = new List<BoardingPass>();
    }

    public enum TicketStatusType
    {
        Confirmed,
        Pending,
        Refundable,
        Nonrefundable,
        Cancelled
    }
}