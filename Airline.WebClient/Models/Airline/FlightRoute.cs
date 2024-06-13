using System.ComponentModel.DataAnnotations;

namespace App.Models.Airline
{

    public class FlightRoute
    {
        [Key]
        public int FlightRouteId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Phải nhập điểm đi")]
        [Display(Name = "Điểm đi")]
        public string DepartureAddress { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Phải nhập điểm đến")]
        [Display(Name = "Điểm đến")]
        public string ArrivalAddress { get; set; }

        [Display(Name = "Chặng bay")]
        public string? FlightSector { get; set; }

        [Display(Name = "Chặng bay")]
        public string? FlightSectorName { get; set; }

        [Display(Name = "Cổng quốc tế/nội địa")]
        public GateType Gate { get; set; }
        
        [Display(Name = "Trạng thái")]
        public StatusType Status { get; set; }

        public enum GateType
        {
            DomesticGate,
            InternationalGate
        }

        public enum StatusType
        {
            Active,
            Inactive
        }
        // N-N relationship with SanBay
        public ICollection<FlightRoute_Airport>? FlightRoute_Airports { get; set; }

        // N-N relationship with ChuyenBay
        public ICollection<FlightRoute_Flight>? FlightRoute_Flights { get; set; }

    }
}