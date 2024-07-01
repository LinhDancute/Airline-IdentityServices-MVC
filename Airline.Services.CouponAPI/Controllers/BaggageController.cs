using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaggageController : ControllerBase
    {
        private readonly IBaggageService _baggageService;

        public BaggageController(IBaggageService baggageService)
        {
            _baggageService = baggageService;
        }

        // GET: api/Baggage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaggageDTO>>> GetAll()
        {
            var baggages = await _baggageService.GetAllAsync();
            return Ok(baggages);
        }

        // GET: api/Baggage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaggageDTO>> GetById(int id)
        {
            try
            {
                var baggage = await _baggageService.GetByIdAsync(id);
                return Ok(baggage);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Baggage
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BaggageCreateDTO baggageCreateDTO)
        {
            if (ModelState.IsValid)
            {
                await _baggageService.CreateAsync(baggageCreateDTO);
                return Ok("Baggage created successfully");
            }
            return BadRequest(ModelState);
        }

        // POST: api/Baggage/CreateBulk
        [HttpPost("CreateBulk")]
        public async Task<IActionResult> CreateBulk([FromBody] IEnumerable<BaggageCreateDTO> baggageCreateDTOs)
        {
            if (ModelState.IsValid)
            {
                await _baggageService.CreateBulkAsync(baggageCreateDTOs);
                return Ok("Bulk baggages created successfully");
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Baggage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _baggageService.DeleteAsync(id);
                return Ok("Baggage deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Baggage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BaggageCreateDTO baggageCreateDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _baggageService.UpdateAsync(id, baggageCreateDTO);
                    return Ok("Baggage updated successfully");
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
