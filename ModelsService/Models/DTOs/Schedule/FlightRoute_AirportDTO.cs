using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.ModelsService.Models.DTOs.Schedule
{
    public class FlightRoute_AirportDTO
    {
        public int FlightRouteID { set; get; }
        public int AirportID { set; get; }
    }
}