using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.CouponAPI.Services;
using Airline.Services.CouponAPI.Services.Implements;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardingPassController : ControllerBase
    {
        private readonly IBoardingPassService _boardingPassService;

        public BoardingPassController(IBoardingPassService boardingPassService)
        {
            _boardingPassService = boardingPassService;
        }

        // GET: api/BoardingPass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardingPassDTO>>> GetAll()
        {
            var boardingPasses = await _boardingPassService.GetAllAsync();
            return Ok(boardingPasses);
        }

        // GET: api/BoardingPass/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardingPassDTO>> GetById(int id)
        {
            try
            {
                var boardingPass = await _boardingPassService.GetByIdAsync(id);
                return Ok(boardingPass);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
