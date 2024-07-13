using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // GET: api/BoardingPass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetAll()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }

        // GET: api/BoardingPass/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetById(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetByIdAsync(id);
                return Ok(invoice);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
