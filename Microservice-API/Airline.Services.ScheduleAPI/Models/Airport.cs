using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Airline.Services.ScheduleAPI.Models
{

    public class Airport
    {
        [Key]
        public int AirportId { get; set; }
        public string AirportName { get; set; }

        [Display(Name = "Tên viết tắt sân bay")]
        public string Abbreviation { get; set; }

        [Display(Name = "Nội dung mô tả sân bay")]
        public string? Description { set; get; }

        [Display(Name = "Phân loại sân bay")]
        public AirportClassification Classification { get; set; }

        [Display(Name = "Trạng thái hoạt động")]
        public AirportStatus Status { get; set; }
        public enum AirportClassification
        {
            Domestic,
            International
        }

        public enum AirportStatus
        {
            Active,
            Closed
        }

        // N-N relationship with TuyenBay
        public ICollection<FlightRoute_Airport>? FlightRoute_Airports { get; } = new List<FlightRoute_Airport>();
    }
}