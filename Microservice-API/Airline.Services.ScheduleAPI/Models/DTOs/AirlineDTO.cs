using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Services.ScheduleAPI.Models.DTOs
{

    public class AirlineDTO
    {
        public int AirlineId { get; set; }
        public int? ParentAirlineId { get; set; }
        public string AirlineName { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public ICollection<AirlineDTO> AirlineChildren { get; set; }
    }
}