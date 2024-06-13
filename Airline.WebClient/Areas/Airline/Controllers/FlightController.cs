using App.Data;
using App.Models;
using App.Models.Airline;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using App.Areas.Airline.Models;
using System.Linq;
using App.Areas.Airline.Models;


namespace App.Areas.Airline.Controllers
{
    [Area("Airline")]
    [Route("admin/airline/flight/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class FlightController : Controller
    {
        private readonly AppDbContext _context;

        public FlightController(AppDbContext context)
        {
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: FlightRoute
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pageSize = 10)
        {
            var flightsQuery = _context.Flights
                .Include(p => p.Airline)
                .OrderByDescending(p => p.Date);

            int totalFlights = await flightsQuery.CountAsync();

            if (pageSize <= 0) pageSize = 10;

            int countPages = (int)Math.Ceiling((double)totalFlights / pageSize);

            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;

            var pagingModel = new PagingModel()
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new { p = pageNumber, pageSize })
            };

            ViewBag.PagingModel = pagingModel;
            ViewBag.TotalFlights = totalFlights;

            ViewBag.flightIndex = (currentPage - 1) * pageSize;


            var flightsInPage = await flightsQuery
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(flightsInPage);
        }

        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.FlightRoute_Flights)
                .FirstOrDefaultAsync(m => m.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }



        // GET: Flight/Create
        public async Task<IActionResult> CreateAsync()
        {
            var aircraft = await _context.Airlines.ToListAsync();
            ViewData["aircraft"] = new MultiSelectList(aircraft, "AirlineId", "AirlineName");

            var flightSector = await _context.FlightRoutes.ToListAsync();
            ViewData["flightSector"] = new MultiSelectList(flightSector, "FlightRouteId", "FlightSector");
            return View();
        }

        // POST: Flight/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AircraftIDs,FlightSectorIDs,Date,FlightNumber,DepartureTime,ArrivalTime,FlightTime,EcoSeat,DeluxeSeat,SkyBossBusinessSeat,SkyBossSeat,Status")] CreateFlightModel flightModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var flight = new Flight
                    {
                        Date = flightModel.Date,
                        FlightNumber = flightModel.FlightNumber,
                        DepartureTime = flightModel.DepartureTime,
                        ArrivalTime = flightModel.ArrivalTime,
                        FlightTime = flightModel.FlightTime,
                        EcoSeat = flightModel.EcoSeat,
                        DeluxeSeat = flightModel.DeluxeSeat,
                        SkyBossBusinessSeat = flightModel.SkyBossBusinessSeat,
                        SkyBossSeat = flightModel.SkyBossSeat,
                        Status = flightModel.Status,
                    };

                    foreach (var aircraftId in flightModel.AircraftIDs)
                    {
                        var airline = await _context.Airlines.FindAsync(aircraftId);

                        if (airline != null)
                        {
                            flight.Aircraft = airline.IATAcode;
                            flight.AirlineId = airline.AirlineId;
                            flight.Airline = airline;
                        }
                    }

                    foreach (var flightSectorId in flightModel.FlightSectorIDs)
                    {
                        var fSector = await _context.FlightRoutes.FindAsync(flightSectorId);

                        if (fSector != null)
                        {
                            flight.FlightSector += $"{fSector.FlightSector}, ";

                            var flightRouteFlight = new FlightRoute_Flight
                            {
                                FlightRouteID = fSector.FlightRouteId,
                            };

                            flight.FlightRoute_Flights.Add(flightRouteFlight);
                        }
                    }

                    // Remove the trailing comma and space if FlightSector is not null
                    if (!string.IsNullOrEmpty(flight.FlightSector))
                    {
                        flight.FlightSector = flight.FlightSector.TrimEnd(',', ' ');
                    }

                    _context.Add(flight);
                    await _context.SaveChangesAsync();

                    StatusMessage = "Vừa tạo chuyến bay mới";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    // Handle exceptions
                }
            }

            // If ModelState is not valid, log the validation errors
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    // Log or debug the validation error messages
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }

            var aircraft = await _context.Airlines.ToListAsync();
            ViewData["aircraft"] = new SelectList(aircraft, "AirlineId", "IATAcode");

            var flightSector = await _context.FlightRoutes.ToListAsync();
            ViewData["flightSector"] = new SelectList(flightSector, "FlightRouteId", "FlightSector");

            return View(flightModel);
        }



        // GET: Flight/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aaircraft = await _context.Airlines.ToListAsync();
            var flight = await _context.Flights.Include(p => p.FlightRoute_Flights).FirstOrDefaultAsync(p => p.FlightId == id);

            if (aaircraft == null || flight == null)
            {
                return NotFound();
            }

            var createFlightModel = new CreateFlightModel()
            {
                FlightId = flight.FlightId,
                AirlineId = flight.AirlineId,
                Date = flight.Date,
                FlightNumber = flight.FlightNumber,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                FlightTime = flight.FlightTime,
                EcoSeat = flight.EcoSeat,
                DeluxeSeat = flight.DeluxeSeat,
                SkyBossBusinessSeat = flight.SkyBossBusinessSeat,
                SkyBossSeat = flight.SkyBossSeat,
                Status = flight.Status,
                FlightSectorIDs = flight.FlightRoute_Flights.Select(pc => pc.FlightRouteID).ToArray(),
                AircraftIDs = flight.AirlineId.HasValue ? new int[] { flight.AirlineId.Value } : new int[] { }
            };

            var aircraft = await _context.Airlines.ToListAsync();
            ViewData["aircraft"] = new SelectList(aircraft, "AirlineId", "AirlineName");

            var flightSector = await _context.FlightRoutes.ToListAsync();
            ViewData["flightSector"] = new SelectList(flightSector, "FlightRouteId", "FlightSector");

            return View(createFlightModel);
        }

        // POST: Flight/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,AirlineId,AircraftIDs,FlightSectorIDs,Date,FlightNumber,DepartureTime,ArrivalTime,FlightTime,EcoSeat,DeluxeSeat,SkyBossBusinessSeat,SkyBossSeat,Status")] CreateFlightModel editFlightModel)
        {
            if (id != editFlightModel.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var flight = await _context.Flights
                        .Include(f => f.FlightRoute_Flights)
                        .FirstOrDefaultAsync(m => m.FlightId == id);

                    if (flight == null)
                    {
                        return NotFound();
                    }

                    flight.AirlineId = editFlightModel.AirlineId;
                    flight.Date = editFlightModel.Date;
                    flight.FlightNumber = editFlightModel.FlightNumber;
                    flight.DepartureTime = editFlightModel.DepartureTime;
                    flight.ArrivalTime = editFlightModel.ArrivalTime;
                    flight.FlightTime = editFlightModel.FlightTime;
                    flight.EcoSeat = editFlightModel.EcoSeat;
                    flight.DeluxeSeat = editFlightModel.DeluxeSeat;
                    flight.SkyBossBusinessSeat = editFlightModel.SkyBossBusinessSeat;
                    flight.SkyBossSeat = editFlightModel.SkyBossSeat;
                    flight.Status = editFlightModel.Status;

                    if (editFlightModel.FlightSectorIDs == null)
                    {
                        flight.FlightRoute_Flights = new List<FlightRoute_Flight>();
                    }
                    else
                    {
                        var oldflightsectorIds = flight.FlightRoute_Flights.Select(c => c.FlightRouteID).ToArray();
                        var newflightsectorIds = editFlightModel.FlightSectorIDs;

                        var removeflightsector = from fsector in flight.FlightRoute_Flights
                                              where (!newflightsectorIds.Contains(fsector.FlightRouteID))
                                              select fsector;
                        _context.FlightRoute_Flights.RemoveRange(removeflightsector);

                        var addflightsectorIds = from fsector in newflightsectorIds
                                         where !oldflightsectorIds.Contains(fsector)
                                         select fsector;

                        foreach (var fsectorId in addflightsectorIds)
                        {
                            _context.FlightRoute_Flights.Add(new FlightRoute_Flight()
                            {
                                FlightID = id,
                                FlightRouteID = fsectorId
                            });
                        }
                    }

                    // Add the new AirlineId
                    var newAirline = await _context.Airlines.FindAsync(editFlightModel.AirlineId);
                    if (newAirline != null)
                    {
                        flight.AirlineId = newAirline.AirlineId;
                        flight.Airline = newAirline;
                    }

                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency exception
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, log the validation errors
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    // Log or debug the validation error messages
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }

            var aircraft = await _context.Airlines.ToListAsync();
            ViewData["aircraft"] = new SelectList(aircraft, "AirlineId", "IATAcode", editFlightModel.AirlineId);

            var flightSector = await _context.FlightRoutes.ToListAsync();
            ViewData["flightSector"] = new MultiSelectList(flightSector, "FlightRouteId", "FlightSector", editFlightModel.FlightSectorIDs);

            return View(editFlightModel);
        }



    



        // GET: FlightRoute/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.FlightId == id);

            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: FlightRoute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightId == id);
        }
    }
}