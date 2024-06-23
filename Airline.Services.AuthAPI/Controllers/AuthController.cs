using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Airline.Services.AuthAPI.Services;
using System.Security.Claims;
using Airline.ModelsService.Models.DTOs.Auth;

namespace Airline.Services.AuthAPI.Controllers
{
    [Area("Auth")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userAccount;

        public AuthController(IAuthService userAccount)
        {
            _userAccount = userAccount;
        }

        [HttpGet("user")]
        [Authorize(Roles = "Administrator")] 
        public async Task<IActionResult> GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var user = await _userAccount.GetUser(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAdmin()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var admin = await _userAccount.GetAdmin(userId);
            if (admin == null)
            {
                return NotFound("Admin not found.");
            }

            return Ok(admin);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO users)
        {
            bool isAdmin = users.IsAdmin;

            var response = await _userAccount.RegisterAccount(users, isAdmin);
            if (!response.flag)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await _userAccount.LoginAccount(loginDTO);
            if (!response.flag)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}
