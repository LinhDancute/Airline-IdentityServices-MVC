using Airline.WebClient.Models.Airline;
using Airline.WebClient.Models.DTOs.Schedule;
using Airline.WebClient.Services.IServices.Airline;
using App.Data;
using App.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.WebClient.Controllers.Airline
{
    [Area("Airline")]
    [Route("admin/airline/flight/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public FlightController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        // GET: admin/airline/flight/Index
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pageSize = 15)
        {
            var dtoList = await _flightService.GetAllFlightsAsync();

            if (dtoList == null)
            {
                TempData["error"] = "Failed to fetch flights.";
                return View(new List<Flight>());
            }

            List<Flight> allFlights = dtoList.Select(dto => _mapper.Map<Flight>(dto)).ToList();

            int totalFlights = allFlights.Count;

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

            var flightsInPage = allFlights
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(flightsInPage);
        }


        // POST: admin/airline/flight/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlightCreateDTO flightDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _flightService.CreateFlightAsync(flightDTO);
                TempData["success"] = "Flight created successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Failed to create flight: {ex.Message}";
                return RedirectToAction(nameof(Index)); // Redirect to index or handle error appropriately
            }
        }

        // GET: admin/airline/flight/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var flight = await _flightService.GetFlightByIdAsync(id);
                if (flight == null)
                {
                    TempData["error"] = "Flight not found";
                    return RedirectToAction(nameof(Index));
                }
                return View(flight); // Adjust to pass appropriate model to view
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Failed to fetch flight details: {ex.Message}";
                return RedirectToAction(nameof(Index)); // Redirect to index or handle error appropriately
            }
        }

        // POST: admin/airline/flight/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FlightCreateDTO flightDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _flightService.UpdateFlightAsync(id, flightDTO);
                TempData["success"] = "Flight updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Failed to update flight: {ex.Message}";
                return RedirectToAction(nameof(Index)); // Redirect to index or handle error appropriately
            }
        }

        // POST: admin/airline/flight/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _flightService.DeleteFlightAsync(id);
                TempData["success"] = "Flight deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Failed to delete flight: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
