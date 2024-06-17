using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Services.ScheduleAPI.Models
{
    [Table("Airline")]
    public class Airline
    {
        [Key]
        public int AirlineId { get; set; }

        // Airline cha (FKey)
        [Display(Name = "Máy bay cha")]
        public int? ParentAirlineId { get; set; }

        [Required]
        [Display(Name = "Tên máy bay")]
        public string? AirlineName { get; set; }

        [Display(Name = "Mã IATA")]
        public string? IATAcode { get; set; }

        [Display(Name = "Mã ICAO")]
        public string? ICAOcode { get; set; }

        [Display(Name = "Nội dung mô tả máy bay")]
        public string? Description { set; get; }

        [Display(Name = "Url hiển thị")]
        public string? Slug { set; get; }
        // Các máy bay con
        public ICollection<Airline>? AirlineChildren { get; set; }

        public Airline()
        {
            AirlineChildren = new HashSet<Airline>();
        }

        [ForeignKey("ParentAirlineId")]
        [Display(Name = "Máy bay cha")]

        public Airline? ParentAirline { set; get; }

        // MayBay - ChuyenBay : n-1
        public ICollection<Flight> Flights { get; } = new List<Flight>();

    }
}