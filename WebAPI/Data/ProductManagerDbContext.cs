using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class ProductManagerDbContext : DbContext
    {
        public ProductManagerDbContext(DbContextOptions<ProductManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
