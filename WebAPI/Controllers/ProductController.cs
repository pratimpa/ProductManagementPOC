using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Services.Abstractions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
         var  Product = await _productService.AddAsync(productDto);

          return CreatedAtAction(nameof(GetById), new { id = Product.ProductId });

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productDto = await _productService.GetById(id);
            if (productDto == null)
            {
                return NotFound();
            }

            return Ok(productDto);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
    }
}
