using App.Data;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace App.Areas.Airline.Controllers
{
    [Area("Airline")]
    [Route("admin/airline/airline/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class AirlineController : Controller
    {
        private readonly AppDbContext _context;

        public AirlineController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Airline/Airline/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var qr = (from a in _context.Airlines select a)
                     .Include(a => a.ParentAirline)
                     .Include(a => a.AirlineChildren);

            var airlines = (await qr.ToListAsync())
                             .Where(a => a.ParentAirline == null)
                             .ToList();

            return View(airlines);
        }

        // GET: /Airline/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airline = await _context.Airlines.Include(a => a.ParentAirline)
                                                 .FirstOrDefaultAsync(m => m.AirlineId == id);
            if (airline == null)
            {
                return NotFound();
            }

            return View(airline);
        }

        private void CreateSelectItems(List<App.Models.Airline.Airline> source, List<App.Models.Airline.Airline> des, int level)
        {
            string prefix = string.Concat(Enumerable.Repeat("----", level));
            foreach (var airline in source)
            {
                des.Add(new App.Models.Airline.Airline()
                {
                    AirlineId = airline.AirlineId,
                    AirlineName = prefix + " " + airline.AirlineName
                });
                // Check if airline.AirlineChildren is not null before iterating
                if (airline.AirlineChildren != null && airline.AirlineChildren.Any())
                {
                    CreateSelectItems(airline.AirlineChildren.ToList(), des, level + 1);
                }

                // else
                // {
                //     Console.WriteLine($"child: {category.CategoryChildren}");
                // }
            }
        }
        // GET: /Airline/Create
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var qr = (from a in _context.Airlines select a)
                            .Include(a => a.ParentAirline)
                            .Include(a => a.AirlineChildren);

            var airlines = (await qr.ToListAsync())
                             .Where(a => a.ParentAirline == null)
                             .ToList();
            airlines.Insert(0, new App.Models.Airline.Airline()
            {
                AirlineId = -1,
                AirlineName = "Không có máy bay cha"
            });

            var items = new List<App.Models.Airline.Airline>();
            CreateSelectItems(airlines, items, 0);
            var selectList = new SelectList(items, "AirlineId", "AirlineName");

            ViewData["ParentAirlineId"] = selectList;
            return View();
        }

        // POST: /Airline/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AirlineName,ICAOcode,IATAcode,Description,Slug,ParentAirlineId")] App.Models.Airline.Airline airline)
        {

            // Ensure ParentAirlineId is set correctly
            Console.WriteLine($"Danh mục cha: {airline.ParentAirlineId}");
            Console.WriteLine($"Ten may bay: {airline.AirlineName}");
            if (airline.ParentAirlineId == -1)
            {
                airline.ParentAirlineId = null; // Set to null if it's ""
            }


            if (ModelState.IsValid)
            {
                if (airline.ParentAirlineId == -1) airline.ParentAirlineId = null;
                _context.Add(airline);
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

            // Retrieve and prepare the data needed for your view
            var qr = (from a in _context.Airlines select a)
                            .Include(a => a.ParentAirline)
                            .Include(a => a.AirlineChildren);

            var airlines = (await qr.ToListAsync())
                             .Where(a => a.ParentAirline == null)
                             .ToList();
            airlines.Insert(0, new App.Models.Airline.Airline()
            {
                AirlineId = -1,
                AirlineName = "Không có máy bay cha"
            });

            var items = new List<App.Models.Airline.Airline>();
            CreateSelectItems(airlines, items, 0);
            var selectList = new SelectList(items, "AirlineId", "AirlineName");

            ViewData["ParentAirlineId"] = selectList;

            // Return to the Create view with validation errors
            return View(airline);
        }

        // GET: /Airline/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airline = await _context.Airlines.FindAsync(id);
            if (airline == null)
            {
                return NotFound();
            }

            var qr = (from a in _context.Airlines select a)
                                        .Include(a => a.ParentAirline)
                                        .Include(a => a.AirlineChildren);

            var airlines = (await qr.ToListAsync())
                             .Where(a => a.ParentAirline == null)
                             .ToList();
            airlines.Insert(0, new App.Models.Airline.Airline()
            {
                AirlineId = -1,
                AirlineName = "Không có máy bay cha"
            });

            var items = new List<App.Models.Airline.Airline>();
            CreateSelectItems(airlines, items, 0);
            var selectList = new SelectList(items, "AirlineId", "AirlineName");

            ViewData["ParentAirlineId"] = selectList;
            return View(airline);
        }

        // POST: /Airline/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AirlineId,AirlineName,ICAOcode,IATAcode,Description,Slug,ParentAirlineId")] App.Models.Airline.Airline airline)
        {
            if (id != airline.AirlineId)
            {
                return NotFound();
            }

            bool canUpdate = true;

            if (airline.ParentAirlineId == airline.AirlineId)
            {
                ModelState.AddModelError(string.Empty, "Phải chọn máy bay cha khác");
                canUpdate = false;
            }

            // Kiem tra thiet lap muc cha phu hop
            if (canUpdate && airline.ParentAirlineId != null)
            {
                var childAirlines =
                            (from c in _context.Airlines select c)
                            .Include(c => c.AirlineChildren)
                            .ToList()
                            .Where(c => c.ParentAirlineId == airline.AirlineId);


                // Func check Id 
                Func<List<App.Models.Airline.Airline>, bool> checkAirlineIds = null;
                checkAirlineIds = (airs) =>
                    {
                        foreach (var air in airs)
                        {
                            Console.WriteLine(air.AirlineName);
                            if (air.AirlineId == airline.ParentAirlineId)
                            {
                                canUpdate = false;
                                ModelState.AddModelError(string.Empty, "Phải chọn máy bay cha khácXX");
                                return true;
                            }
                            if (air.AirlineChildren != null)
                                return checkAirlineIds(air.AirlineChildren.ToList());

                        }
                        return false;
                    };
                // End Func 
                checkAirlineIds(childAirlines.ToList());
            }

            if (ModelState.IsValid && canUpdate)
            {
                try
                {
                    if (airline.ParentAirlineId == -1)
                        airline.ParentAirlineId = null;

                    var dtc = _context.Airlines.FirstOrDefault(c => c.AirlineId == id);
                    _context.Entry(dtc).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                    _context.Update(airline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirlineExists(airline.AirlineId))
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

            var qr = (from a in _context.Airlines select a)
                                        .Include(a => a.ParentAirline)
                                        .Include(a => a.AirlineChildren);

            var airlines = (await qr.ToListAsync())
                             .Where(a => a.ParentAirline == null)
                             .ToList();
            airlines.Insert(0, new App.Models.Airline.Airline()
            {
                AirlineId = -1,
                AirlineName = "Không có máy bay cha"
            });

            var items = new List<App.Models.Airline.Airline>();
            CreateSelectItems(airlines, items, 0);
            var selectList = new SelectList(items, "AirlineId", "AirlineName");

            ViewData["ParentAirlineId"] = selectList;
            return View(airline);
        }

        // GET: /Airline/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airline = await _context.Airlines.Include(m => m.ParentAirline)
                                                 .FirstOrDefaultAsync(m => m.AirlineId == id);
            if (airline == null)
            {
                return NotFound();
            }

            return View(airline);
        }

        // POST: /Airline/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var airline = await _context.Airlines.Include(m => m.ParentAirline)
                                                 .FirstOrDefaultAsync(m => m.AirlineId == id);

            if (airline == null)
            {
                return NotFound();
            }
            foreach (var aAirline in airline.AirlineChildren)
            {
                aAirline.ParentAirlineId = airline.ParentAirlineId;
            }

            _context.Airlines.Remove(airline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirlineExists(int id)
        {
            return _context.Airlines.Any(e => e.AirlineId == id);
        }
    }
}