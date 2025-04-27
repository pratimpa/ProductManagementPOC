using WebAPI.DTOs;

namespace WebAPI.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> AddAsync(CategoryDto dto);
        Task<bool> UpdateAsync(int id, CategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
