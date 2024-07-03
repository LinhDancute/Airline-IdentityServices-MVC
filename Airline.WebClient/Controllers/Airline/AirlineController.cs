using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Models.DTOs.Schedule;
using Airline.WebClient.Services.IServices.Airline;
using App.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Airline.WebClient.Controllers.Airline
{
    [Area("Airline")]
    [Route("admin/airline/airline/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class AirlineController : Controller
    {
        private readonly IAirlineService _airlineService;
        private IMapper _mapper;

        public AirlineController(
            IMapper mapper,
            IAirlineService airlineService)
        {
            _mapper = mapper;
            _airlineService = airlineService;
        }

        public async Task<IActionResult> Index()
        {
            List<Models.Airline.Airline> list = new List<Models.Airline.Airline>();

            var dtoList = await _airlineService.GetAllAirlinesAsync();

            if (dtoList != null)
            {
                list = dtoList.Select(dto => _mapper.Map<Models.Airline.Airline>(dto)).ToList();
            }
            else
            {
                TempData["error"] = "Failed to fetch airlines.";
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _airlineService.GetAirlineByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var ticketClass = _mapper.Map<Models.Airline.Airline>(dto);
            return View(ticketClass);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await ParentAirlinesViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AirlineDTO model)
        {
            if (ModelState.IsValid)
            {
                await _airlineService.CreateAirlineAsync(model);
                TempData["success"] = "Airline created successfully";
                return RedirectToAction(nameof(Index));
            }
            await ParentAirlinesViewBag();
            var airline = _mapper.Map<Models.Airline.Airline>(model);
            return View(model);
        }

        private async Task ParentAirlinesViewBag()
        {
            var airlines = await _airlineService.GetAllAirlinesAsync();
            ViewBag.ParentAirlineId = new SelectList(airlines, "AirlineId", "AirlineName");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _airlineService.GetAirlineByIdAsync(id);
            if (model != null)
            {
                var airline = _mapper.Map<Models.Airline.Airline>(model);
                return View(airline);
            }
            TempData["error"] = "Airline not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AirlineDTO model)
        {
            if (ModelState.IsValid)
            {
                await _airlineService.UpdateAirlineAsync(id, model);
                TempData["success"] = "Airline updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _airlineService.GetAirlineByIdAsync(id);
            if (model != null)
            {
                var ticketClass = _mapper.Map<Models.Airline.Airline>(model);
                return View(ticketClass);
            }
            TempData["error"] = "Airline not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _airlineService.DeleteAirlineAsync(id);
            TempData["success"] = "Airline deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
