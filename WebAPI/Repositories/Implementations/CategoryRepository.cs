using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Repositories.Implementations
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ProductManagerDbContext _db;

        public CategoryRepository(ProductManagerDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Category entity)
        {
            await _db.Categories.AddAsync(entity);
        }

        public void Delete(int id)
        {
            var category = _db.Categories.Find(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
            }
        }

        public async Task<Category?> Get(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Update(Category entity)
        {
            _db.Categories.Update(entity);
        }
    }
}

