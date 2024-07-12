using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.ScheduleAPI.Services;
using Airline.Services.ScheduleAPI.Services.ServiceImpl;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.ScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        // GET: api/flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightCreateDTO>>> GetAllFlights()
        {
            try
            {
                var flights = await _flightService.GetAllFlightsAsync();
                return Ok(flights);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // POST: api/flight
        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] FlightCreateDTO flightDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _flightService.CreateFlightAsync(flightDTO);
                return Ok(new { Success = true, Message = "Flight created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // POST: api/flight/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateFlightsBulk([FromBody] List<FlightCreateDTO> flightDTOs)
        {
            try
            {
                if (flightDTOs == null || flightDTOs.Count == 0)
                {
                    return BadRequest(new { Success = false, Message = "Flight data is null or empty" });
                }

                await _flightService.CreateFlightsAsync(flightDTOs);

                return CreatedAtAction(nameof(GetAllFlights), new { }, new { Success = true, Message = "Flights created successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // GET: api/flight/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightDTO>> GetFlightRById(int id)
        {
            try
            {
                var flight = await _flightService.GetFlightByIdAsync(id);
                if (flight == null)
                {
                    return NotFound(new { Success = false, Message = "Flight not found." });
                }
                return Ok(flight);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{flightId}")]
        public async Task<IActionResult> UpdateFlight(int flightId, [FromBody] FlightCreateDTO flightDTO)
        {
            try
            {
                await _flightService.UpdateFlightAsync(flightId, flightDTO);
                return Ok("Flight updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{flightId}/close")]
        public async Task<IActionResult> CloseFlight(int flightId)
        {
            try
            {
                await _flightService.CloseFlightAsync(flightId);
                return Ok("Flight closed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{flightId}")]
        public async Task<IActionResult> DeleteFlight(int flightId)
        {
            try
            {
                await _flightService.DeleteFlightAsync(flightId);
                return Ok("Flight deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchFlights([FromBody] FlightSearchDTO flightSearchDTO)
        {
            try
            {
                var flights = await _flightService.SearchFlightsByRouteAsync(flightSearchDTO);
                return Ok(flights);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
