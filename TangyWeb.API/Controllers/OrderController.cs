using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tangy.Business.Respositories.Interface;
using Tangy.Models;

namespace TangyWeb.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
    }
}
