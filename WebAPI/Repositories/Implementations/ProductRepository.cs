using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductManagerDbContext _db;

        public ProductRepository(ProductManagerDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Product entity)
        {
            await _db.Products.AddAsync(entity);
        }

        public void Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
            }
        }

        public async Task<Product?> Get(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Update(Product entity)
        {
            _db.Products.Update(entity);
        }
    }
}
