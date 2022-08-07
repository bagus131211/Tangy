using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tangy.Models;

namespace TangyWeb.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        [ActionName("create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO payment)
        {
            try
            {
                var client = _configuration.GetValue<string>("TangyClientUri");

                var options = new SessionCreateOptions
                {
                    SuccessUrl = client + payment.SuccessUrl,
                    CancelUrl = client + payment.CancelUrl,
                    LineItems = new(),
                    Mode = "payment",
                    PaymentMethodTypes = new() { "card" }
                };

                foreach (var detail in payment.Order.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)detail.Price * 100, //20.00 -> 2000
                            Currency = "idr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = detail.Product.Name
                            }
                        },
                        Quantity = detail.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                var session = service.Create(options);
                return Ok(new SuccessDTO { Data = session.Id +';'+ session.PaymentIntentId });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDTO { ErrorMessage = ex.Message });
            }
        }
    }
}
