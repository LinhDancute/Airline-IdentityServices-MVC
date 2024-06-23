using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using App.Data;

namespace App.Areas.Airport.Controllers
{
    [Area("Airline")]
    [Route("admin/airline/airport/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]

    public class AirportController : Controller
{
        private readonly AppDbContext _context;

        public AirportController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Airport
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var airports = await _context.Airports.ToListAsync();
            return View(airports);
        }

        // GET: /Airport/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports.FirstOrDefaultAsync(m => m.AirportId == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: /Airport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AirportName,Abbreviation,Description,Classification,Status")] Models.Airline.Airport airport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airport);
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
            return View(airport);
        }

        // GET: /Airport/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports.FindAsync(id);

            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: /Airport/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AirportId,AirportName,Abbreviation,Description,Classification,Status")] Models.Airline.Airport airport)
        {
            if (id != airport.AirportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportExists(airport.AirportId))
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
            return View(airport);
        }

        // GET: /Airport/Delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports.FirstOrDefaultAsync(m => m.AirportId == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: /Airport/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airport = await _context.Airports.FindAsync(id);
            _context.Airports.Remove(airport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportExists(int id)
        {
            return _context.Airports.Any(e => e.AirportId == id);
        }
    }
}