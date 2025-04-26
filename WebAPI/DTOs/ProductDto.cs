using System.ComponentModel.DataAnnotations;
using WebAPI.Models;

namespace WebAPI.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? Category { get; set; }

    }
}
