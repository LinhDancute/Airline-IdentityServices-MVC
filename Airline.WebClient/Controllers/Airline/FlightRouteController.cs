using Airline.WebClient.Models.Airline;
using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Models.DTOs.Schedule;
using Airline.WebClient.Services.IServices.Airline;
using App.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.WebClient.Controllers.Airline
{
    [Area("Airline")]
    [Route("admin/airline/flightroute/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class FlightRouteController : Controller
    {
        private readonly IFlightRouteService _flightRouteService;
        private readonly IAirportService _airportService;
        private IMapper _mapper;

        public FlightRouteController(
            IMapper mapper,
            IFlightRouteService flightRouteService,
            IAirportService airportService)
        {
            _mapper = mapper;
            _flightRouteService = flightRouteService;
            _airportService = airportService;
        }

        public async Task<IActionResult> Index()
        {
            List<FlightRoute> list = new List<FlightRoute>();

            var dtoList = await _flightRouteService.GetAllFlightRoutesAsync();

            if (dtoList != null)
            {
                list = dtoList.Select(dto => _mapper.Map<FlightRoute>(dto)).ToList();
            }
            else
            {
                TempData["error"] = "Failed to fetch flight route.";
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _flightRouteService.GetFlightRouteByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var flightRoute = _mapper.Map<FlightRoute>(dto);
            return View(flightRoute);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FlightRouteCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _flightRouteService.CreateFlightRouteAsync(model);
                TempData["success"] = "Flight route created successfully";
                return RedirectToAction(nameof(Index));
            }

            var airports = await _airportService.GetAllAirportsAsync();
            ViewBag.Airports = airports.Select(a => new { Abbreviation = a.Abbreviation, Name = a.AirportName }).ToList();

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var model = await _flightRouteService.GetFlightRouteByIdAsync(id);
            if (model != null)
            {
                var flightRoute = _mapper.Map<FlightRoute>(model);
                return View(flightRoute);
            }
            TempData["error"] = "Flight route not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FlightRouteCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _flightRouteService.UpdateFlightRouteAsync(id, model);
                    TempData["success"] = "Flight route updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Failed to update flight route: {ex.Message}";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _flightRouteService.GetFlightRouteByIdAsync(id);
            if (model != null)
            {
                var flightRoute = _mapper.Map<FlightRoute>(model);
                return View(flightRoute);
            }
            TempData["error"] = "Flight route not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _flightRouteService.DeleteFlightRouteAsync(id);
                TempData["success"] = "Flight route deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Failed to delete flight route: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
