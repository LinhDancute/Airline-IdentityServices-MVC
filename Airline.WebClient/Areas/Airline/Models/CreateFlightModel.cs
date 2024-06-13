using System.ComponentModel.DataAnnotations;
using App.Models.Airline;

namespace App.Areas.Airline.Models {
    public class CreateFlightModel : Flight
    {
        [Required(ErrorMessage = "Phải có {0}")]
        [Display(Name = "Máy bay")]
        public int[] AircraftIDs { get; set; }

        [Required(ErrorMessage = "Phải có {0}")]
        [Display(Name = "Chặng bay")]
        public int[] FlightSectorIDs { get; set; }
    }
}
