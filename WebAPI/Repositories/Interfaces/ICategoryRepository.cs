using WebAPI.Models;

namespace WebAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> Get(int id);
        Task AddAsync(Category entity);
        void Update(Category entity);
        void Delete(int id);
        Task<int> SaveChangesAsync();
    }
}
