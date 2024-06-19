using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Services.ScheduleAPI.Models
{

    [Table("FlightRoute_Airports")]
    public class FlightRoute_Airport
    {
        public int FlightRouteID { set; get; }
        public int AirportID { set; get; }


        [ForeignKey("FlightRouteID")]
        public FlightRoute FlightRoutes { set; get; }

        [ForeignKey("AirportID")]
        public Airport Airports { set; get; }
    }

}