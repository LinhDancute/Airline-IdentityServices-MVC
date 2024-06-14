﻿namespace Airline.Services.ScheduleAPI.Models.DTOs
{
    public class AirportCreateDTO
    {
        public string AirportName { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public Airport.AirportClassification Classification { get; set; }
        public Airport.AirportStatus Status { get; set; }
    }
}
