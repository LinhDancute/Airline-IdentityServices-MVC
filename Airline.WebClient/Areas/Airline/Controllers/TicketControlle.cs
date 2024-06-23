using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models.Airline;
using Microsoft.AspNetCore.Authorization;
using App.Models;

namespace App.Areas.Airline.Controllers
{
    [Area("Airline")]
    [Route("admin/airline/ticket/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class TicketController : Controller
    {
        private readonly AppDbContext _context;
        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Ticket> tickets = await _context.Tickets
                .Include(t => t.Flight) 
                .ToListAsync();

            return View(tickets);
        }
    }
}