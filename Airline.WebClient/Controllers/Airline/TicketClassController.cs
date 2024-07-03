using Airline.WebClient;
using Airline.WebClient.Models.Airline;
using Airline.WebClient.Models.DTOs.Coupon;
using Airline.WebClient.Models.DTOs;
using App.Data;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using Airline.WebClient.Services.IServices.Airline;

namespace Airline.WebClient.Controllers.Airline
{
    [Area("Airline")]
    [Route("admin/airline/ticketclass/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class TicketClassController : Controller
    {
        private readonly ITicketClassService _ticketClassService;
        private IMapper _mapper;

        public TicketClassController(
            IMapper mapper,
            ITicketClassService ticketClassService)
        {
            _mapper = mapper;
            _ticketClassService = ticketClassService;
        }

        public async Task<IActionResult> Index()
        {
            List<TicketClass> list = new List<TicketClass>();

            var dtoList = await _ticketClassService.GetAllAsync();

            if (dtoList != null)
            {
                list = dtoList.Select(dto => _mapper.Map<TicketClass>(dto)).ToList();
            }
            else
            {
                TempData["error"] = "Failed to fetch ticket classes.";
            }

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _ticketClassService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var ticketClass = _mapper.Map<TicketClass>(dto);
            return View(ticketClass);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketClassCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _ticketClassService.CreateAsync(model);
                TempData["success"] = "Ticket class created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _ticketClassService.GetByIdAsync(id);
            if (model != null)
            {
                var ticketClass = _mapper.Map<TicketClass>(model);
                return View(ticketClass);
            }
            TempData["error"] = "Ticket class not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TicketClassCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                await _ticketClassService.UpdateAsync(id, model);
                TempData["success"] = "Ticket class updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _ticketClassService.GetByIdAsync(id);
            if (model != null)
            {
                var ticketClass = _mapper.Map<TicketClass>(model);
                return View(ticketClass);
            }
            TempData["error"] = "Ticket class not found";
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
        await _ticketClassService.DeleteAsync(id);
        TempData["success"] = "Ticket class deleted successfully";
        return RedirectToAction(nameof(Index));
        }
    }
}