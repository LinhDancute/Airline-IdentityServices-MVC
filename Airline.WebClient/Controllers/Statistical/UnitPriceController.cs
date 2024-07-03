using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Models.Statistical;
using Airline.WebClient.Services.IServices.Airline;
using App.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.WebClient.Controllers.Statistical
{
    [Area("Statistical")]
    [Route("admin/statistical/unitprice/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class UnitPriceController : Controller
    {
        private readonly IUnitPriceService _unitPriceService;
        private IMapper _mapper;

        public UnitPriceController(
            IMapper mapper,
            IUnitPriceService unitPriceService)
        {
            _mapper = mapper;
            _unitPriceService = unitPriceService;
        }

        public async Task<IActionResult> Index()
        {
            List<UnitPrice> list = new List<UnitPrice>();

            var dtoList = await _unitPriceService.GetAllAsync();

            if (dtoList != null)
            {
                list = dtoList.Select(dto => _mapper.Map<UnitPrice>(dto)).ToList();
            }
            else
            {
                TempData["error"] = "Failed to fetch unit prices.";
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _unitPriceService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var unitPrice = _mapper.Map<UnitPrice>(dto);
            return View(unitPrice);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UnitPriceCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _unitPriceService.CreateAsync(model);
                TempData["success"] = "Unit price created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _unitPriceService.GetByIdAsync(id);
            if (model != null)
            {
                var unitPrice = _mapper.Map<UnitPrice>(model);
                return View(unitPrice);
            }
            TempData["error"] = "Unit price not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UnitPriceCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _unitPriceService.UpdateAsync(id, model);
                TempData["success"] = "Unit price updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitPriceService.GetByIdAsync(id);
            if (model != null)
            {
                var unitPrice = _mapper.Map<UnitPrice>(model);
                return View(unitPrice);
            }
            TempData["error"] = "Unit price not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitPriceService.DeleteAsync(id);
            TempData["success"] = "Unit price deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
