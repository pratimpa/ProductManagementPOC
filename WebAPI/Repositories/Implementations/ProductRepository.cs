using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repositories.Implementations
{
    public class ProductRepository
    {
        ProductManagerDbContext _db; 
        public ProductRepository(ProductManagerDbContext db)
        {
            _db = db;
        }

        public async void Add(Product entity)
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

        public async Task<Product?> Get(int Id)
        {
            return await _db.Products.FindAsync(Id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Update(Product entity)
        {
            _db.Update(entity);
        }
    }
}
