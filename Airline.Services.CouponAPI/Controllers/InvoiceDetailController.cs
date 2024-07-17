using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.Services.CouponAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailService _invoiceDetailService;

        public InvoiceDetailController(IInvoiceDetailService invoiceDetailService)
        {
            _invoiceDetailService = invoiceDetailService;
        }

        // GET: api/BoardingPass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDetailDTO>>> GetAll()
        {
            var invoiceDetails = await _invoiceDetailService.GetAllAsync();
            return Ok(invoiceDetails);
        }

        // GET: api/BoardingPass/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetailDTO>> GetById(int id)
        {
            try
            {
                var invoiceDetail = await _invoiceDetailService.GetByIdAsync(id);
                return Ok(invoiceDetail);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
