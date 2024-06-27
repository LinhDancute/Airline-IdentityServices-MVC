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
    [Route("admin/airline/airport/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class AirportController : Controller
    {
        private readonly IAirportService _airportService;
        private IMapper _mapper;

        public AirportController(
            IMapper mapper,
            IAirportService airportService)
        {
            _mapper = mapper;
            _airportService = airportService;
        }

        public async Task<IActionResult> Index()
        {
            List<Airport> list = new List<Airport>();

            var dtoList = await _airportService.GetAllAirportsAsync();

            if (dtoList != null)
            {
                list = dtoList.Select(dto => _mapper.Map<Airport>(dto)).ToList();
            }
            else
            {
                TempData["error"] = "Failed to fetch ticket classes.";
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _airportService.GetAirportByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var ticketClass = _mapper.Map<Airport>(dto);
            return View(ticketClass);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AirportCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _airportService.CreateAirportAsync(model);
                TempData["success"] = "Airport created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _airportService.GetAirportByIdAsync(id);
            if (model != null)
            {
                var airport = _mapper.Map<Airport>(model);
                return View(airport);
            }
            TempData["error"] = "Airport not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AirportCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _airportService.UpdateAirportAsync(id, model);
                TempData["success"] = "Airport updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _airportService.GetAirportByIdAsync(id);
            if (model != null)
            {
                var airport = _mapper.Map<Airport>(model);
                return View(airport);
            }
            TempData["error"] = "Airport not found";
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _airportService.DeleteAirportAsync(id);
            TempData["success"] = "Airport deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Close(int id)
        {
            var model = await _airportService.GetAirportByIdAsync(id);
            if (model != null)
            {
                var airport = _mapper.Map<Airport>(model);
                return View(airport);
            }
            TempData["error"] = "Airport not found";
            return NotFound();
        }

        [HttpPost, ActionName("Close")]
        public async Task<IActionResult> CloseConfirmed(int id, AirportCreateDTO model)
        {
            await _airportService.CloseAirportAsync(id, _mapper.Map<AirportDTO>(model));
            TempData["success"] = "Airport closed successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
