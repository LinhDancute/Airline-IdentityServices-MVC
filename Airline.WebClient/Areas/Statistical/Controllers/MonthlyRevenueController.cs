using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Statistical.Controllers
{
    [Area("Statistical")]
    [Route("admin/statistical/monthlyrevenue/[action]/{id?}")]
    [Authorize(Roles = RoleName.Administrator)]
    public class MonthlyRevenueController : Controller
    {
        private readonly AppDbContext _context;
        public MonthlyRevenueController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var monthlyRevenues = await _context.MonthlyRevenues.ToListAsync();
            return View(monthlyRevenues);
        }
    }
}