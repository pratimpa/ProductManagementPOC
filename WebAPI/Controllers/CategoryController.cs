using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Services.Abstractions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
    //    [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
        {
            var category = await _categoryService.AddAsync(categoryDto);
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoryDto = await _categoryService.GetByIdAsync(id);
            if (categoryDto == null) return NotFound();
            return Ok(categoryDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
        int PageNumber = 1,
                int PageSize = 10,
                string sortBy = "Name",
                string SortDirection = "asc")
        {

            var products = await _categoryService.GetAllAsync();
            return Ok(products);
        }

        [HttpPut("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _categoryService.UpdateAsync(id, categoryDto);
            if (!updated) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(deleted);
        }
    }
}
