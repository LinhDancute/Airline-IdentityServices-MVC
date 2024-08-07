﻿using Airline.ModelsService;
using Airline.ModelsService.Models.Airline;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Airline.Services.ScheduleAPI.Repositories.RepositoryImpl
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AppDbContext _context;

        public FlightRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> GetByIdAsync(int id)
        {
            return await _context.Flights.SingleOrDefaultAsync(a => a.FlightId == id);
        }

        //Add signle
        public async Task AddAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        //Add list
        public async Task AddRangeAsync(IEnumerable<Flight> flights)
        {
            await _context.Flights.AddRangeAsync(flights);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Flight flight)
        {
            _context.Entry(flight).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> FlightExistsAsync(int id)
        {
            return await _context.Flights.AnyAsync(e => e.FlightId == id);
        }

        public async Task<Flight> FindAsync(Expression<Func<Flight, bool>> predicate)
        {
            return await _context.Flights.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(DateTime date, string flightSector)
        {
            return await _context.Flights
                .Where(f => f.FlightSector == flightSector &&
                            f.Date.Date == date.Date)
                .ToListAsync();
        }
        public async Task<IEnumerable<FlightRoute>> FindFlightRoutesAsync(string partialDepartureAddress, string partialArrivalAddress)
        {
            var departureAbbreviation = await _context.Airports
                .Where(a => a.AirportName.Contains(partialDepartureAddress))
                .Select(a => a.Abbreviation)
                .FirstOrDefaultAsync();

            var arrivalAbbreviation = await _context.Airports
                .Where(a => a.AirportName.Contains(partialArrivalAddress))
                .Select(a => a.Abbreviation)
                .FirstOrDefaultAsync();

            return await _context.FlightRoutes
                .Where(fr => fr.DepartureAddress == departureAbbreviation && fr.ArrivalAddress == arrivalAbbreviation)
                .ToListAsync();
        }
    }
}
