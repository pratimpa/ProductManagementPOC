using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Services.Abstractions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            var product = await _productService.AddAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productDto = await _productService.GetByIdAsync(id);
            if (productDto == null) return NotFound();
            return Ok(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
        int PageNumber = 1,
                int PageSize = 10,
                string sortBy = "Name",
                string SortDirection = "asc")
        {

            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpPut("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _productService.UpdateAsync(id, productDto);
            if (!updated) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(deleted);
        }
    }

}
