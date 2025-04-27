using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.Abstractions;

namespace WebAPI.Services.Implementations
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CategoryDto> AddAsync( CategoryDto categoryDto)
        {
            var entity = _mapper.Map<Category>(categoryDto);

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            categoryDto.CategoryId = entity.CategoryId;
            return categoryDto;
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repo.Get(id);
            if (category == null) return false;

            _repo.Delete(id);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repo.Get(id);
            if (category == null) return null;

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateAsync(int id, CategoryDto dto)
        {
            var category = await _repo.Get(id);
            if (category == null) return false;

            category.Name = dto.Name;
            

            _repo.Update(category);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
