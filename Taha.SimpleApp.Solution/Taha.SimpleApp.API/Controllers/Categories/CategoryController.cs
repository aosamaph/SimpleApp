using Microsoft.AspNetCore.Mvc;
using Taha.SimpleApp.Application.Services.Categories;
using Taha.SimpleApp.Domain.Exceptions;

namespace Taha.SimpleApp.API.Controllers.Categories
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType<IEnumerable<CategoryDto>>(200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<CategoryDto> categories = _categoryService.GetAll();
                return Ok(categories);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType<CategoryDto>(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            try
            {
                CategoryDto categories = _categoryService.Get(id);
                return Ok(categories);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType<int>(201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] string categoryName)
        {
            try
            {
                var id = _categoryService.Create(categoryName);
                return CreatedAtAction(nameof(Get), id);
            }
            catch (DuplicateCategoryException)
            {
                return Conflict("Category with the same name already exists.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromQuery] bool deleteProducts = false)
        {
            var result = _categoryService.Delete(id, deleteProducts);
            if (result)
            {
                return NoContent();
            }
            return StatusCode(500, "An error occurred while deleting the category.");
        }
    }
}
