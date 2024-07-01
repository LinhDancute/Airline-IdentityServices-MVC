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
    [Route("admin/airline/meal/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IMapper _mapper;

        public MealController(
            IMapper mapper,
            IMealService mealService)
        {
            _mapper = mapper;
            _mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            List<Meal> list = new List<Meal>();

            var dtoList = await _mealService.GetAllAsync();

            if (dtoList != null)
            {
                list = dtoList.Select(dto => _mapper.Map<Meal>(dto)).ToList();
            }
            else
            {
                TempData["error"] = "Failed to fetch meals.";
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _mealService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var meal = _mapper.Map<Meal>(dto);
            return View(meal);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MealCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _mealService.CreateAsync(model);
                TempData["success"] = "Meal created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _mealService.GetByIdAsync(id);
            if (model != null)
            {
                var meal = _mapper.Map<Meal>(model);
                return View(meal);
            }
            TempData["error"] = "Meal not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MealCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _mealService.UpdateAsync(id, model);
                TempData["success"] = "Meal updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _mealService.GetByIdAsync(id);
            if (model != null)
            {
                var meal = _mapper.Map<Meal>(model);
                return View(meal);
            }
            TempData["error"] = "Meal not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mealService.DeleteAsync(id);
            TempData["success"] = "Meal deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
