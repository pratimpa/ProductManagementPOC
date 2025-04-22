using System.Collections.Generic;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Services.Abstractions
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetById(int id);
        Task<Product> AddAsync(ProductDto product);
        void Update(ProductDto product);
        void Delete(int id);
    }
}
