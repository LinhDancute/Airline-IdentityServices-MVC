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
    [Route("admin/airline/ticketclass/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class TicketClassController : Controller
    {
        private readonly AppDbContext _context;
        public TicketClassController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var ticketClass = await _context.TicketClasses.ToListAsync();
            return View(ticketClass);
        }

        // GET: /Ticketclass/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketclass = await _context.TicketClasses.FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticketclass == null)
            {
                return NotFound();
            }

            return View(ticketclass);
        }

        // POST: /TicketClass/Create
        [HttpPost, ActionName(nameof(Create))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketName,Description")] App.Models.Airline.TicketClass ticketClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, log the validation errors
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    // Log or debug the validation error messages
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }
            return View(ticketClass);
        }

        // GET: /ticketClass/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var ticketClass = await _context.TicketClasses.FindAsync(id);

            if (ticketClass == null)
            {
                return NotFound();
            }

            return View(ticketClass);
        }

        // POST: /ticketClass/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,TicketName,Description")] App.Models.Airline.TicketClass ticketClass)
        {
            if (id != ticketClass.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketClassExists(ticketClass.TicketId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticketClass);
        }

        // GET: /ticketClass/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketClass = await _context.TicketClasses.FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticketClass == null)
            {
                return NotFound();
            }

            return View(ticketClass);
        }

        // POST: /ticketClass/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketClass = await _context.TicketClasses.FindAsync(id);
            _context.TicketClasses.Remove(ticketClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketClassExists(int id)
        {
            return _context.TicketClasses.Any(e => e.TicketId == id);
        }
    }
}