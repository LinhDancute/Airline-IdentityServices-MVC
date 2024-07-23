using Airline.ModelsService;
using Airline.ModelsService.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Airline.Duende.IdentityServer.IDbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(AppDbContext context,
                             UserManager<AppUser> userManager,
                             RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(Config.Administrator).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(Config.Administrator)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Config.Manager)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Config.Member)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            AppUser admin = new()
            {
                UserName = "linhdanAdmin",
                Email = "linhdanAdmin@example.com",
                EmailConfirmed = true,
                PhoneNumber = "123",
                CMND = "0987123456789",
            };

            _userManager.CreateAsync(admin, "123").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, Config.Administrator).GetAwaiter().GetResult();

            _userManager.AddClaimsAsync(admin, new Claim[] {
                new Claim(JwtClaimTypes.Name, admin.UserName),
                new Claim(JwtClaimTypes.Role, Config.Administrator),
                new Claim(JwtClaimTypes.Role, Config.Manager)

            }).GetAwaiter().GetResult();
        }
    }
}
