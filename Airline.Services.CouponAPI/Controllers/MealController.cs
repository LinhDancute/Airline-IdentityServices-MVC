using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        // GET: api/Meal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MealDTO>>> GetAll()
        {
            var meals = await _mealService.GetAllAsync();
            return Ok(meals);
        }

        // GET: api/Meal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealDTO>> GetById(int id)
        {
            var meal = await _mealService.GetByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return Ok(meal);
        }

        // POST: api/Meal
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MealCreateDTO mealCreateDTO)
        {
            try
            {
                await _mealService.CreateAsync(mealCreateDTO);
                return Ok("Meal created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Meal/Bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> CreateBulk([FromBody] IEnumerable<MealCreateDTO> mealCreateDTOs)
        {
            try
            {
                await _mealService.CreateBulkAsync(mealCreateDTOs);
                return Ok("Meals created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Meal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MealCreateDTO mealCreateDTO)
        {
            try
            {
                await _mealService.UpdateAsync(id, mealCreateDTO);
                return Ok("Meal updated successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Meal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mealService.DeleteAsync(id);
                return Ok("Meal deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
