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
        public GateStatusType Gate { get; set; }
        
        [Display(Name = "Trạng thái")]
        public FlightRouteStatusType Status { get; set; }

        public enum GateStatusType
        {
            DomesticGate,
            InternationalGate
        }

        public enum FlightRouteStatusType
        {
            Active,
            Inactive
        }
        // N-N relationship with Airport
        public ICollection<FlightRoute_Airport>? FlightRoute_Airports { get; set; }

        // N-N relationship with Flight
        public ICollection<FlightRoute_Flight>? FlightRoute_Flights { get; set; }

    }
}