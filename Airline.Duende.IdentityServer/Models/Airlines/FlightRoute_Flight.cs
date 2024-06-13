using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.WebClient.Models.Airlines
{

    [Table("FlightRoute_Flight")]
    public class FlightRoute_Flight
    {
        public int FlightRouteID { set; get; }
        public int FlightID { set; get; }


        [ForeignKey("FlightRouteID")]
        public FlightRoute FlightRoute { set; get; }

        [ForeignKey("FlightID")]
        public Flight Flight { set; get; }
    }

}