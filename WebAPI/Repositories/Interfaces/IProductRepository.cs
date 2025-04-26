using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IProductRepository
    {

        Task<List<Product>> GetAllAsync();
        Task<Product?> Get(int id);
        Task AddAsync(Product entity);
        void Update(Product entity);
        void Delete(int id);
        Task<int> SaveChangesAsync();
    }
}
