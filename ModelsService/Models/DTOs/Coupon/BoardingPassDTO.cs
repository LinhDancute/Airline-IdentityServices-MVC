﻿using Airline.ModelsService.Models.Airline;
using System.ComponentModel.DataAnnotations;

namespace Airline.ModelsService.Models.DTOs.Coupon
{
    public class BoardingPassDTO
    {
        public int BoardingPassId { get; set; }
        public int TicketId { get; set; }
        public string PNR { get; set; }
        public string ETicket { set; get; }
        public DateTime Date { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string Seat { get; set; }
        public string Gate { get; set; }
        public TimeSpan BoardingTime { get; set; }
        public string FlightNumber { get; set; }
        public string? PassengerName { get; set; }
        public string Class { get; set; }
        public string BaggageType { get; set; }
        public string SSR { get; set; }
        public string Itinerary { get; set; }
        public bool Published { get; set; }
        public Ticket Ticket { get; set; }
    }
}