using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.ScheduleAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.ScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }

        // GET: api/airline
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirlineDTO>>> GetAirlines()
        {
            try
            {
                var airlines = await _airlineService.GetAllAirlinesAsync();
                return Ok(new ResponsesDTO { Success = true, Result = airlines });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }

        // GET: api/airline/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AirlineDTO>> GetAirlineById(int id)
        {
            try
            {
                var airline = await _airlineService.GetAirlineByIdAsync(id);
                if (airline == null)
                    return NotFound(new ResponsesDTO { Success = false, Message = $"Airline with ID {id} not found" });

                return Ok(new ResponsesDTO { Success = true, Result = airline });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }

        // POST: api/airline
        [HttpPost]
        public async Task<ActionResult<AirlineDTO>> CreateAirline([FromBody] AirlineDTO airlineDTO)
        {
            try
            {
                if (airlineDTO == null)
                    return BadRequest(new ResponsesDTO { Success = false, Message = "Airline data is null" });

                await _airlineService.CreateAirlineAsync(airlineDTO);
                return CreatedAtAction(nameof(GetAirlineById), new { id = airlineDTO.AirlineId }, new ResponsesDTO { Success = true, Result = airlineDTO });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ResponsesDTO { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }



        // PUT: api/airline/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAirline(int id, [FromBody] AirlineDTO airlineDTO)
        {
            try
            {
                if (airlineDTO == null || id != airlineDTO.AirlineId)
                    return BadRequest(new ResponsesDTO { Success = false, Message = "Invalid airline data or ID mismatch" });

                await _airlineService.UpdateAirlineAsync(id, airlineDTO);
                return Ok(new ResponsesDTO { Success = true, Message = $"Airline with ID {id} updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }

        // DELETE: api/airline/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirline(int id)
        {
            try
            {
                await _airlineService.DeleteAirlineAsync(id);
                return Ok(new ResponsesDTO { Success = true, Message = $"Airline with ID {id} deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }
    }
}
