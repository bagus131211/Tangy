using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tangy.Business.Respositories.Interface;
using Tangy.Models;

namespace TangyWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAll());
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
            var product = await _productRepository.GetById(id.Value);
            if (product is null)
            {
                return BadRequest(new ErrorDTO()
                {
                    ErrorMessage = "Data not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(product);
        }
    }
}
