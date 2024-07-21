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

        [HttpPost("register/member")]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterDTO registerDTO)
        {
            var result = await _userAccount.RegisterMemberAccount(registerDTO);
            if (result.flag)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO registerDTO)
        {
            var result = await _userAccount.RegisterAdminAccount(registerDTO);
            if (result.flag)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
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

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await _userAccount.GetUser(id);
            if (result.flag)
            {
                return Ok(result);
            }
            return NotFound(result.Message);
        }

        [HttpGet("admin/{id}")]
        public async Task<IActionResult> GetAdmin(string id)
        {
            var result = await _userAccount.GetAdmin(id);
            if (result.flag)
            {
                return Ok(result);
            }
            return NotFound(result.Message);
        }

        [Authorize]
        [HttpPut("updatePhoneNumber")]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdatePhoneNumberDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User not found.");
            }

            var response = await _userAccount.UpdatePhoneNumber(userId, model.PhoneNumber);

            if (response.flag)
            {
                return Ok(new { Message = response.Message });
            }
            else
            {
                return BadRequest(new { Message = response.Message });
            }
        }

        [HttpGet("currenUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _userAccount.GetCurrentUser(userId);
            if (!response.flag)
            {
                return NotFound(response.Message);
            }
            return Ok(response.AccountDTO.First());
        }
    }
}

