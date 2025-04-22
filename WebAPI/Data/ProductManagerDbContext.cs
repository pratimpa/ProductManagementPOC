
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ProductManagerDbContext :DbContext
    {
        public ProductManagerDbContext(DbContextOptions<ProductManagerDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
