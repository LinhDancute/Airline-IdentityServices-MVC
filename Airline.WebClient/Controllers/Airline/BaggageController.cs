using Airline.WebClient.Models.Airline;
using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Services.IServices.Airline;
using App.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.WebClient.Controllers.Airline
{
    [Area("Airline")]
    [Route("admin/airline/baggage/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class BaggageController : Controller
    {
        private readonly IBaggageService _baggageService;
        private readonly IMapper _mapper;

        public BaggageController(
            IMapper mapper,
            IBaggageService baggageService)
        {
            _mapper = mapper;
            _baggageService = baggageService;
        }

        public async Task<IActionResult> Index()
        {
            List<Baggage> list = new List<Baggage>();

            var dtoList = await _baggageService.GetAllAsync();

            if (dtoList != null)
            {
                list = dtoList.Select(dto => _mapper.Map<Baggage>(dto)).ToList();
            }
            else
            {
                TempData["error"] = "Failed to fetch meals.";
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _baggageService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var meal = _mapper.Map<Baggage>(dto);
            return View(meal);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BaggageCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _baggageService.CreateAsync(model);
                TempData["success"] = "Baggage created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _baggageService.GetByIdAsync(id);
            if (model != null)
            {
                var baggage = _mapper.Map<Baggage>(model);
                return View(baggage);
            }
            TempData["error"] = "Baggage not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BaggageCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _baggageService.UpdateAsync(id, model);
                TempData["success"] = "Baggage updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _baggageService.GetByIdAsync(id);
            if (model != null)
            {
                var baggage = _mapper.Map<Baggage>(model);
                return View(baggage);
            }
            TempData["error"] = "Baggage not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _baggageService.DeleteAsync(id);
            TempData["success"] = "Baggage deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
