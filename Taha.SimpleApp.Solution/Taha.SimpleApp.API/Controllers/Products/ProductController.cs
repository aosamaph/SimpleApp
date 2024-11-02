using Microsoft.AspNetCore.Mvc;
using Taha.SimpleApp.Application.Services.Products;
using Taha.SimpleApp.Domain.Exceptions;

namespace Taha.SimpleApp.API.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Category/{categoryId}")]
        [ProducesResponseType<IEnumerable<ProductDto>>(200)]
        [ProducesResponseType(404)]
        public IActionResult GetProducts(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    return BadRequest();

                IEnumerable<ProductDto> products = _productService.GetProducts(categoryId);
                return Ok(products);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType<ProductDto>(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            try
            {
                ProductDto product = _productService.GetProduct(id);
                return Ok(product);
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType<int>(201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ProductDto productDto)
        {
            if (productDto == null || string.IsNullOrWhiteSpace(productDto.Name) || productDto.Price <= 0)
            {
                return BadRequest("Invalid product data.");
            }

            try
            {
                var productId = _productService.CreateProduct(productDto.CategoryId, productDto.Name, productDto.Price, productDto.Currency);
                return CreatedAtAction(nameof(Get), new { id = productId }, productId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null)
            {
                return BadRequest("Invalid product data.");
            }

            try
            {
                _productService.UpdateDescription(updateProductDto.Id, updateProductDto.Description);
                _productService.UpdateImage(updateProductDto.Id, updateProductDto.Image);

                return NoContent();
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
