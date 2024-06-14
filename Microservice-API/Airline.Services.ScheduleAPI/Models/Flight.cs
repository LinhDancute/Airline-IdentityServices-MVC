using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Services.ScheduleAPI.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }

        public int? AirlineId { get; set; }

        [Display(Name = "Máy bay")]
        public string? Aircraft { set; get; }

        // số hiệu chuyến bay
        [Required]
        [Display(Name = "Số hiệu chuyến bay")]
        public string? FlightNumber { get; set; }

        [Display(Name = "Chặng bay")]
        public string? FlightSector { get; set; }

        [Required]
        [Display(Name = "Giờ bay")]
        public float? FlightTime { get; set; }

        [Required]
        [Display(Name = "Giờ khởi hành")]
        public TimeSpan? DepartureTime { get; set; }

        [Required]
        [Display(Name = "Giờ đến")]
        public TimeSpan? ArrivalTime { get; set; }

        [Display(Name = "Ngày/giờ ")]
        public DateTime Date { get; set; }

        [Display(Name = "Số ghế hạng tiêu chuẩn")]
        public int? EcoSeat { get; set; }

        [Display(Name = "Số ghế hạng phổ thông đặc biệt")]
        public int? DeluxeSeat { get; set; }

        [Display(Name = "Số ghế hạng cao cấp")]
        public int? SkyBossSeat { get; set; }

        [Display(Name = "Số ghế hạng thương gia")]
        public int? SkyBossBusinessSeat { get; set; }
        
        [Column("Status")]
        [Display(Name = "Trạng thái hoạt động")]
        public FlightStatusType Status { get; set; }

        public enum FlightStatusType
        {
            Active,
            Inactive
        }

        public Airline? Airlines { get; set; }

        public ICollection<FlightRoute_Flight>? FlightRoute_Flights { get; set; } = new List<FlightRoute_Flight>();
    }
}