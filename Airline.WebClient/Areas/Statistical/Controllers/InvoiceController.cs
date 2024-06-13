using App.Data;
using App.Models;
using App.Models.Statistical;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Statistical.Controllers
{
    [Area("Statistical")]
    [Route("admin/statistical/invoice/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;
        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Invoice> invoices = await _context.Invoices
                .Include(i => i.MonthlyRevenue)
                .Include(i => i.Passenger)
                .ToListAsync();

            return View(invoices);
        }
    }
}