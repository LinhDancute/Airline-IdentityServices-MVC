using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Services.ScheduleAPI.Models
{

    [Table("FlightRoute_Flight")]
    public class FlightRoute_Flight
    {
        public int FlightRouteID { set; get; }
        public int FlightID { set; get; }


        [ForeignKey("FlightRouteID")]
        public FlightRoute FlightRoutes { set; get; }

        [ForeignKey("FlightID")]
        public Flight Flights { set; get; }
    }

}