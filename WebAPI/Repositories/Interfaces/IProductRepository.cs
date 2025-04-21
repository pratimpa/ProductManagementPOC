using WebAPI.Models;

namespace WebAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
       Task<List<Product>> GetAllAsync();
       Task<Product?> Get(int Id);
        void Add(Product entity);
        void Update(Product entity);
        void Delete(int id);
        Task<int> SaveChangesAsync();
    }
}
