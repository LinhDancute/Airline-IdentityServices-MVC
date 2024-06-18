﻿using Airline.Services.ScheduleAPI.Models;
using Airline.Services.ScheduleAPI.Models.DTOs;

namespace Airline.Services.ScheduleAPI.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightDTO>> GetAllFlightsAsync();
        Task CreateFlightAsync(FlightCreateDTO flightDTO); //add signle
        Task CreateFlightsAsync(List<FlightCreateDTO> flightDTOs); //add list
        Task UpdateFlightAsync(int flightId, FlightCreateDTO flightDTO);
        Task CloseFlightAsync(int flightId); //close flight, Staus Active -> Closed
        Task DeleteFlightAsync(int flightId);
    }
}
