using System.Collections.Generic;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Services.Abstractions
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync(int pagenumber, int pagesize);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> AddAsync(ProductDto dto);
        Task<bool> UpdateAsync(int id, ProductDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
