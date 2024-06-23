using Airline.Services.CouponAPI.Services;
using Airline.WebClient.Models.DTOs.Coupon;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketClassController : ControllerBase
    {
        private readonly ITicketClassService _service;

        public TicketClassController(ITicketClassService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketClassDTO>>> GetAll()
        {
            var ticketClasses = await _service.GetAllAsync();
            return Ok(ticketClasses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketClassDTO>> GetById(int id)
        {
            try
            {
                var ticketClass = await _service.GetByIdAsync(id);
                return Ok(ticketClass);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TicketClassCreateDTO ticketClassDTO)
        {
            try
            {
                await _service.CreateAsync(ticketClassDTO);
                return Ok("Ticket class created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("bulk")]
        public async Task<ActionResult> CreateBulk([FromBody] IEnumerable<TicketClassCreateDTO> ticketClassDTOs)
        {
            try
            {
                await _service.CreateBulkAsync(ticketClassDTOs);
                return Ok("Ticket classes created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TicketClassCreateDTO ticketClassDTO)
        {
            try
            {
                await _service.UpdateAsync(id, ticketClassDTO);
                return Ok("Ticket class updated successfully.");
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
                await _service.DeleteAsync(id);
                return Ok("Ticket class deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
