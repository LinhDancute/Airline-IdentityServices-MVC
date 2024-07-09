using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> GetAllTickets()
        {
            try
            {
                var tickets = await _ticketService.GetAllAsync();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // GET: api/ticket/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDTO>> GetTicketById(int id)
        {
            try
            {
                var ticket = await _ticketService.GetByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound(new { Success = false, Message = "Ticket not found." });
                }
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // POST: api/ticket
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDTO ticketDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _ticketService.CreateAsync(ticketDTO);
                return Ok(new { Success = true, Message = "Ticket created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // POST: api/ticket/bulk
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateTicketsBulk([FromBody] List<TicketCreateDTO> ticketDTOs)
        {
            try
            {
                if (ticketDTOs == null || ticketDTOs.Count == 0)
                {
                    return BadRequest(new { Success = false, Message = "Ticket data is null or empty" });
                }

                await _ticketService.CreateBulkAsync(ticketDTOs, User.Identity.Name);

                return CreatedAtAction(nameof(GetAllTickets), new { }, new { Success = true, Message = "Tickets created successfully" });
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

        // PUT: api/ticket/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] TicketCreateDTO ticketDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _ticketService.UpdateAsync(id, ticketDTO);
                return Ok(new { Success = true, Message = "Ticket updated successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }

        // DELETE: api/ticket/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                await _ticketService.DeleteAsync(id);
                return Ok(new { Success = true, Message = "Ticket deleted successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = ex.Message });
            }
        }
    }
}
