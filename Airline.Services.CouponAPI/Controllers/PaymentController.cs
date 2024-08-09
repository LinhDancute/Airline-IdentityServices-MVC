using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Airline.ModelsService.Models.Payment;

namespace Airline.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [HttpPost("create-checkout-session")]
        public async Task<ActionResult> CreateCheckoutSession([FromBody] PaymentRequest request)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long?) (request.Amount * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Flight Ticket",
                        },
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:4200/cancel",
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { id = session.Id });
        }
    }
}
