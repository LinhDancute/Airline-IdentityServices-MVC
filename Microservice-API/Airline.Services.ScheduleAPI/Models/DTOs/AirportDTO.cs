using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Airline.Services.ScheduleAPI.Models.DTOs
{

    public class AirportDTO
    {
        public int AirportId { get; set; }

        public string AirportName { get; set; }

        public string Abbreviation { get; set; }

        public string? Description { set; get; }

        public AirportClassificationType Classification { get; set; }

        public AirportStatusType Status { get; set; }
        public ICollection<FlightRoute_Airport>? FlightRoute_Airports { get; set; }
        public enum AirportClassificationType
        {
            Domestic,
            International
        }

        public enum AirportStatusType
        {
            Active,
            Closed
        }
    }
}