using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Airline.WebClient.Models.Airline;

namespace Airline.WebClient.Models.DTOs.Schedule
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