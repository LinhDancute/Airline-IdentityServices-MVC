using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.ScheduleAPI.Services;
using Airline.Services.ScheduleAPI.Services.ServiceImpl;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.ScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;

        public AirportController(IAirportService airportService, IMapper mapper)
        {
            _airportService = airportService;
            _mapper = mapper;
        }

        // GET: api/airport
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<AirportDTO>>> GetAirports()
        {
            try
            {
                var airports = await _airportService.GetAllAirportsAsync();
                return Ok(new ResponsesDTO { Success = true, Result = airports });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }

        // GET: api/airport/{id}
        [HttpGet("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AirportDTO>> GetAirportById(int id)
        {
            try
            {
                var airport = await _airportService.GetAirportByIdAsync(id);
                if (airport == null)
                    return NotFound(new ResponsesDTO { Success = false, Message = $"Airport with ID {id} not found" });

                return Ok(new ResponsesDTO { Success = true, Result = airport });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }

        // POST: api/airport
        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AirportDTO>> CreateAirport([FromBody] AirportCreateDTO airportDTO)
        {
            try
            {
                if (airportDTO == null)
                {
                    return BadRequest(new ResponsesDTO { Success = false, Message = "Airport data is null" });
                }

                await _airportService.CreateAirportAsync(airportDTO);

                var createdAirport = await _airportService.GetAirportByNameAsync(airportDTO.AirportName);

                if (createdAirport == null)
                {
                    return BadRequest(new ResponsesDTO { Success = false, Message = "Failed to retrieve created airport." });
                }

                return CreatedAtAction(nameof(GetAirportById), new { id = createdAirport.AirportId }, new ResponsesDTO { Success = true, Result = _mapper.Map<AirportDTO>(createdAirport) });
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

        // POST: api/airport/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult> CreateAirports([FromBody] List<AirportCreateDTO> airportDTOs)
        {
            try
            {
                if (airportDTOs == null || airportDTOs.Count == 0)
                {
                    return BadRequest(new ResponsesDTO { Success = false, Message = "Airport data is null or empty" });
                }

                await _airportService.CreateAirportsAsync(airportDTOs);

                return Ok(new ResponsesDTO { Success = true, Message = "Airports created successfully" });
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


        // PUT: api/airport/{id}
        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateAirport(int id, [FromBody] AirportCreateDTO airportDTO)
        {
            try
            {
                if (airportDTO == null)
                    return BadRequest(new ResponsesDTO { Success = false, Message = "Invalid airport data or ID mismatch" });

                await _airportService.UpdateAirportAsync(id, airportDTO);
                return Ok(new ResponsesDTO { Success = true, Message = $"Airport with ID {id} updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }

        // DELETE: api/airport/{id}
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAirport(int id)
        {
            try
            {
                await _airportService.DeleteAirportAsync(id);
                return Ok(new ResponsesDTO { Success = true, Message = $"Airport with ID {id} deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponsesDTO { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("close/{id}")]
        public async Task<IActionResult> CloseAirport(int id, [FromBody] AirportCreateDTO airportCreateDTO)
        {
            try
            {
                // Map AirportCreateDTO to AirportDTO
                var airportDTO = _mapper.Map<AirportDTO>(airportCreateDTO);

                await _airportService.CloseAirportAsync(id, airportDTO);

                return Ok(new { Success = true, Message = "Airport closed successfully" });
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

