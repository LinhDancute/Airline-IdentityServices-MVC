using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.Models.Airline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace App.Areas.Airline.Controllers
{
    [Area("Airline")]
    [Route("admin/airline/flightroute/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class FlightRouteController : Controller
    {
        private readonly AppDbContext _context;

        public FlightRouteController(AppDbContext context)
        {
            _context = context;
        }
        // GET: FlightRoute
        public async Task<IActionResult> Index()
        {
            var flightRoutes = await _context.FlightRoutes.ToListAsync();
            return View(flightRoutes);
        }

        // GET: FlightRoute/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightRoute = await _context.FlightRoutes
                .FirstOrDefaultAsync(m => m.FlightRouteId == id);

            if (flightRoute == null)
            {
                return NotFound();
            }

            return View(flightRoute);
        }

        // GET: FlightRoute/Create
        public IActionResult Create()
        {
            ViewBag.Airports = _context.Airports.ToList();
            return View();
        }

        // POST: FlightRoute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartureAddress,ArrivalAddress,FlightSector,FlightSectorName,Gate,Status")] FlightRoute flightRoute)
        {
            if (ModelState.IsValid)
            {

                var departureAirport = await _context.Airports.FirstOrDefaultAsync(a => a.Abbreviation == flightRoute.DepartureAddress);
                var arrivalAirport = await _context.Airports.FirstOrDefaultAsync(a => a.Abbreviation == flightRoute.ArrivalAddress);
                
                if (departureAirport != null && departureAirport.Status == App.Models.Airline.Airport.AirportStatus.Closed)
                {
                    ModelState.AddModelError("DepartureAddress", "Điểm đi đã đóng cửa.");
                }

                if (arrivalAirport != null && arrivalAirport.Status == App.Models.Airline.Airport.AirportStatus.Closed)
                {
                    ModelState.AddModelError("ArrivalAddress", "Điểm đến đã đóng cửa.");
                }

                if (ModelState.IsValid)
                {
                    var existingRoute = await _context.FlightRoutes
                        .FirstOrDefaultAsync(fr => fr.FlightRouteId != flightRoute.FlightRouteId
                        && fr.DepartureAddress == flightRoute.DepartureAddress
                        && fr.ArrivalAddress == flightRoute.ArrivalAddress);

                    if (existingRoute != null)
                    {
                        ModelState.AddModelError(string.Empty, "Chuyến bay đã tồn tại với cùng điểm đi và điểm đến.");
                        ViewBag.Airports = _context.Airports.ToList();
                        return View(flightRoute);
                    }

                    if (flightRoute.DepartureAddress == flightRoute.ArrivalAddress)
                    {
                        ModelState.AddModelError("ArrivalAddress", "Điểm đi phải khác điểm đến.");
                        ViewBag.Airports = _context.Airports.ToList();
                        return View(flightRoute);
                    }

                    flightRoute.FlightSector = $"{flightRoute.DepartureAddress}-{flightRoute.ArrivalAddress}";

                    var departureAirportName = (await _context.Airports.FirstOrDefaultAsync(a => a.Abbreviation == flightRoute.DepartureAddress))?.AirportName;
                    var arrivalAirportName = (await _context.Airports.FirstOrDefaultAsync(a => a.Abbreviation == flightRoute.ArrivalAddress))?.AirportName;

                    flightRoute.FlightSectorName = $"{departureAirportName} - {arrivalAirportName}";

                    var flightRouteAirport = new FlightRoute_Airport
                    {
                        FlightRouteID = flightRoute.FlightRouteId, 
                        AirportID = departureAirport.AirportId
                    };

                    _context.Add(flightRoute);
                    _context.Add(flightRouteAirport);

                    flightRoute.FlightRoute_Airports = new List<FlightRoute_Airport> { flightRouteAirport };

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    // Log or debug the validation error messages
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }

            ViewBag.Airports = _context.Airports.ToList();
            return View(flightRoute);
        }
        
        // GET: FlightRoute/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightRoute = await _context.FlightRoutes.FindAsync(id);

            if (flightRoute == null)
            {
                return NotFound();
            }

            // Ensure that ViewBag.Airports is properly populated
            ViewBag.Airports = _context.Airports.ToList();

            return View(flightRoute);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightRouteId,DepartureAddress,ArrivalAddress,FlightSector,FlightSectorName,Gate,Status")] FlightRoute flightRoute)
        {
            if (id != flightRoute.FlightRouteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var departureAirport = await _context.Airports.FirstOrDefaultAsync(a => a.Abbreviation == flightRoute.DepartureAddress);
                var arrivalAirport = await _context.Airports.FirstOrDefaultAsync(a => a.Abbreviation == flightRoute.ArrivalAddress);

                // Check if either the departure or arrival airport is closed
                if (departureAirport != null && departureAirport.Status == App.Models.Airline.Airport.AirportStatus.Closed)
                {
                    ModelState.AddModelError("DepartureAddress", "Điểm đi đã đóng cửa.");
                }

                if (arrivalAirport != null && arrivalAirport.Status == App.Models.Airline.Airport.AirportStatus.Closed)
                {
                    ModelState.AddModelError("ArrivalAddress", "Điểm đến đã đóng cửa.");
                }

                if (flightRoute.DepartureAddress == flightRoute.ArrivalAddress)
                {
                    ModelState.AddModelError("ArrivalAddress", "Điểm đi phải khác điểm đến.");
                }

                var existingRoute = await _context.FlightRoutes
                    .FirstOrDefaultAsync(fr => fr.FlightRouteId != flightRoute.FlightRouteId
                                        && fr.DepartureAddress == flightRoute.DepartureAddress
                                        && fr.ArrivalAddress == flightRoute.ArrivalAddress);

                if (existingRoute != null)
                {
                    ModelState.AddModelError(string.Empty, "Chuyến bay đã tồn tại với cùng điểm đi và điểm đến.");
                    // Ensure that ViewBag.Airports is properly populated
                    ViewBag.Airports = _context.Airports.ToList();
                    return View(flightRoute);
                }   

                if (ModelState.IsValid)
                {
                    try
                    {
                        flightRoute.FlightSector = $"{flightRoute.DepartureAddress}-{flightRoute.ArrivalAddress}";

                        var departureAirportName = (await _context.Airports.FirstOrDefaultAsync(a => a.Abbreviation == flightRoute.DepartureAddress))?.AirportName;
                        var arrivalAirportName = (await _context.Airports.FirstOrDefaultAsync(a => a.Abbreviation == flightRoute.ArrivalAddress))?.AirportName;

                        flightRoute.FlightSectorName = $"{departureAirportName} - {arrivalAirportName}";

                        _context.Update(flightRoute);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!FlightRouteExists(flightRoute.FlightRouteId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            // Repopulate ViewBag.Airports for the view
            ViewBag.Airports = _context.Airports.ToList();
            return View(flightRoute);
        }



        // GET: FlightRoute/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightRoute = await _context.FlightRoutes
                .FirstOrDefaultAsync(m => m.FlightRouteId == id);

            if (flightRoute == null)
            {
                return NotFound();
            }

            return View(flightRoute);
        }

        // POST: FlightRoute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightRoute = await _context.FlightRoutes.FindAsync(id);
            _context.FlightRoutes.Remove(flightRoute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightRouteExists(int id)
        {
            return _context.FlightRoutes.Any(e => e.FlightRouteId == id);
        }
    }
}