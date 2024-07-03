using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitPriceController : ControllerBase
    {
        private readonly IUnitPriceService _unitPriceService;

        public UnitPriceController(IUnitPriceService unitPriceService)
        {
            _unitPriceService = unitPriceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitPriceDTO>>> GetAll()
        {
            var unitPrices = await _unitPriceService.GetAllAsync();
            return Ok(unitPrices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitPriceDTO>> GetById(int id)
        {
            try
            {
                var unitPrice = await _unitPriceService.GetByIdAsync(id);
                return Ok(unitPrice);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UnitPriceCreateDTO unitPriceCreateDTO)
        {
            try
            {
                var createdUnitPrice = await _unitPriceService.CreateAsync(unitPriceCreateDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdUnitPrice.PriceId }, "Unit price created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("bulk")]
        public async Task<ActionResult> CreateBulk([FromBody] IEnumerable<UnitPriceCreateDTO> unitPriceCreateDTOs)
        {
            try
            {
                await _unitPriceService.CreateBulkAsync(unitPriceCreateDTOs);
                return Ok("Unit prices created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UnitPriceCreateDTO unitPriceCreateDTO)
        {
            try
            {
                await _unitPriceService.UpdateAsync(id, unitPriceCreateDTO);
                return Ok("Unit price updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _unitPriceService.DeleteAsync(id);
                return Ok("Unit price deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
