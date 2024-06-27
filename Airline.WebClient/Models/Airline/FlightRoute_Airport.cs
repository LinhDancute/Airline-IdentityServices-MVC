using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.WebClient.Models.Airline
{
    public class FlightRoute_Airport
    {
        public int FlightRouteID { set; get; }
        public int AirportID { set; get; }


        [ForeignKey("FlightRouteID")]
        public FlightRoute FlightRoute { set; get; }

        [ForeignKey("AirportID")]
        public Airport Airport { set; get; }
    }

}