using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Airline
{
    [Table("Airline")]

    public class Airline
    {
        [Key]
        public int AirlineId { get; set; }

        // Airline cha (FKey)
        [Display(Name = "Máy bay cha")]
        public int? ParentAirlineId { get; set; }

        // tên máy bay
        [Required(ErrorMessage = "Phải nhập tên máy bay")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} dài {1} đến {2}")]
        [Display(Name = "Tên máy bay")]
        public string? AirlineName { get; set; }

        // mã code máy bay
        [Display(Name = "Mã IATA")]
        public string? IATAcode { get; set; }

        // mã code máy bay
        [Display(Name = "Mã ICAO")]
        public string? ICAOcode { get; set; }

        // Nội dung, thông tin chi tiết về máy bay
        [DataType(DataType.Text)]
        [Display(Name = "Nội dung mô tả máy bay")]
        public string? Description { set; get; }

        //chuỗi Url
        [Required(ErrorMessage = "Phải tạo url")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} dài {1} đến {2}")]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9-]")]
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