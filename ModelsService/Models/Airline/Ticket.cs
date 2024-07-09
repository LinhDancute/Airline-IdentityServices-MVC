using Airline.ModelsService.Models.Statistical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Airline.ModelsService.Models.Airline
{
    public class Ticket{
        [Key]
        public int TicketId { get; set; }
        public string? PassengerId { set; get; }
        public int FlightId { get; set; }
        public int PriceId { get; set; }
        public int ClassId { get; set; }

        [Display(Name = "Tên hành khách")]
        public string PassengerName { get; set; }

        [Display(Name = "SDT hành khách")]
        public string PassengerPhoneNumber { get; set; }

        [Display(Name = "Chặng bay")]
        public string Itinerary { get; set; }

        [Display(Name = "Số hiệu máy bay")]
        public string FlightNumber { get; set; }

        [Display(Name = "Ngày bay")]
        public DateTime Date { get; set; }

        [Display(Name = "Thời gian bay")]
        public TimeSpan? DepartureTime { get; set; }

        [Display(Name = "Ghế đặt chỗ")]
        public string Seat { get; set; }

        [Display(Name = "Hạng vé")]
        public string Class { get; set; }

        [Display(Name = "Mã PNR")]
        public string PNR { get; set; }

        [Display(Name = "Dịch vụ bữa ăn")]
        public string MealRequest { get; set; }

        [Display(Name = "Loại hành lý")]
        public string BaggageType { get; set; }

        [Display(Name = "Đơn giá USD")]
        public string USD { get; set; }

        [Display(Name = "Đơn giá VND")]
        public string VND { get; set; }
        public TicketStatusType Status { get; set; }
        public AppUser? Passenger { set; get; }
        public Flight Flight { get; set; }
        public UnitPrice UnitPrice { get; set; }
        public TicketClass TicketClass { get; set; }
        public ICollection<BoardingPass>? BoardingPasses { get; } = new List<BoardingPass>();
        public ICollection<Ticket_Meal>? Ticket_Meals { get; set; } = new List<Ticket_Meal>();
        public ICollection<Ticket_Baggage>? Ticket_Baggages { get; set; } = new List<Ticket_Baggage>();
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