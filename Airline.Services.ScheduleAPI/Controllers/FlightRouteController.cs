using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.ScheduleAPI.Services;
using Airline.Services.ScheduleAPI.Services.ServiceImpl;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.ScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightRouteController : ControllerBase
    {
        private readonly IFlightRouteService _flightRouteService;
        private readonly IMapper _mapper;

        public FlightRouteController(IFlightRouteService flightRouteService, IMapper mapper)
        {
            _flightRouteService = flightRouteService;
            _mapper = mapper;
        }

        // GET: api/flightroute
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightRouteDTO>>> GetAllFlightRoutes()
        {
            try
            {
                var flightRoutes = await _flightRouteService.GetAllFlightRoutesAsync();
                return Ok(flightRoutes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // GET: api/flightroute/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightRouteDTO>> GetFlightRouteById(int id)
        {
            try
            {
                var flightRoute = await _flightRouteService.GetFlightRouteByIdAsync(id);
                if (flightRoute == null)
                {
                    return NotFound(new { Success = false, Message = "Flight route not found." });
                }
                return Ok(flightRoute);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // POST: api/flightroute
        [HttpPost]
        public async Task<IActionResult> CreateFlightRoute([FromBody] FlightRouteCreateDTO flightRouteDTO)
        {
            try
            {
                if (flightRouteDTO == null)
                {
                    return BadRequest(new { Success = false, Message = "Flight route data is null" });
                }

                await _flightRouteService.CreateFlightRouteAsync(flightRouteDTO);

                var createdFlightRoute = await _flightRouteService.GetAirportByAbbreviationAsync(flightRouteDTO.DepartureAddress);
                if (createdFlightRoute == null)
                {
                    return NotFound(new { Success = false, Message = $"Airport with abbreviation '{flightRouteDTO.DepartureAddress}' not found." });
                }

                return Ok(new { Success = true, Message = "Flight route created successfully." });
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

        // POST: api/flightroute/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateFlightRoutesBulk([FromBody] List<FlightRouteCreateDTO> flightRouteDTOs)
        {
            try
            {
                if (flightRouteDTOs == null || flightRouteDTOs.Count == 0)
                {
                    return BadRequest(new { Success = false, Message = "Flight route data is null or empty" });
                }

                await _flightRouteService.CreateFlightRoutesAsync(flightRouteDTOs);

                return CreatedAtAction(nameof(GetAllFlightRoutes), new { }, new { Success = true, Message = "Flight routes created successfully" });
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

        // PUT: api/flightroute/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlightRoute(int id, [FromBody] FlightRouteCreateDTO flightRouteDTO)
        {
            try
            {
                await _flightRouteService.UpdateFlightRouteAsync(id, flightRouteDTO);
                return Ok(new { Success = true, Message = "Flight route updated successfully" });
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

        // PUT: api/flightroute/close/{id}
        [HttpPut("close/{id}")]
        public async Task<IActionResult> CloseFlightRoute(int id, [FromBody] FlightRouteCreateDTO flightRouteDTO)
        {
            try
            {
                await _flightRouteService.CloseFlightRouteAsync(id, flightRouteDTO);
                return Ok(new { Success = true, Message = "Flight route closed successfully" });
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

        // DELETE: api/flightroute/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlightRoute(int id)
        {
            try
            {
                await _flightRouteService.DeleteFlightRouteAsync(id);
                return Ok(new { Success = true, Message = "Flight route deleted successfully" });
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
    }
}
