using App.Data;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Airline.Controllers
{
    [Area("Airline")]
    [Route("admin/airline/boardingpass/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class BoardingPassController : Controller
    {
        private readonly AppDbContext _context;
        public BoardingPassController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var boardingPasses = await _context.BoardingPasses.ToListAsync();
            return View(boardingPasses);
        }
    }
}