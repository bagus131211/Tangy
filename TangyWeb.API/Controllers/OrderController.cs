using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tangy.Business.Respositories.Interface;
using Tangy.Models;

namespace TangyWeb.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IOrderRepository _orderRepository;
        readonly IEmailSender _mailer;

        public OrderController(IOrderRepository orderRepository, IEmailSender mailer)
        {
            _orderRepository = orderRepository;
            _mailer = mailer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id is null or 0)
            {
                return BadRequest(new ErrorDTO()
                {
                    ErrorMessage = "Invalid id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var order = await _orderRepository.GetById(id.Value);
            if (order is null)
            {
                return BadRequest(new ErrorDTO()
                {
                    ErrorMessage = "Data not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(order);
        }

        [HttpPost]
        [ActionName("create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO payment)
        {
            payment.Order.OrderHeader.OrderDate = DateTime.Now;
            var result = await _orderRepository.Create(payment.Order);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("markpayment")]
        public async Task<IActionResult> MarkMyPayment([FromBody] OrderHeaderDTO orderHeader)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(orderHeader.SessionId);
            if (sessionDetails.PaymentStatus is "paid")
            {
                var result = await _orderRepository.MarkPaymentAsSuccessful(orderHeader.Id);
                if (result is null)
                {
                    return BadRequest(new ErrorDTO { ErrorMessage = "Can not mark payment as successful" });
                }
                await _mailer.SendEmailAsync(result.Email, "Tangy Order Confirmation", "New payment has been issued : " + result.Id);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
