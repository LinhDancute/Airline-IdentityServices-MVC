using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Airline
{

    [Table("FlightRoute_Airport")]
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