using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using App.Data;
namespace App.Areas.Statistical.Controllers
{
    [Area("Statistical")]
    [Route("admin/statistical/unitprice/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class UnitPriceController : Controller
    {
        private readonly AppDbContext _context;
        public UnitPriceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var unitPrices = await _context.UnitPrices.ToListAsync();
            return View(unitPrices);
        }
    }
}