using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Repositories.Implementations;
using WebAPI.Services.Abstractions;

namespace WebAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductDto> AddAsync(ProductDto dto)
        {
            var entity = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId

            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            dto.ProductId = entity.ProductId;
            return dto;
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repo.Get(id);
            if (product == null) return false;

            _repo.Delete(id);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductDto>> GetAllAsync(int pagenumber, int pagesize)
        {
            var products = await _repo.GetAllAsync();


            int skip = (pagenumber - 1) * pagesize;

            var pagedData =  products
                            .OrderBy(x => x.ProductId) // important: always ORDER BY something
                            .Skip(skip)
                            .Take(pagesize)
                            .ToList();
            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repo.Get(id);
            if (product == null) return null;

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        public async Task<bool> UpdateAsync(int id, ProductDto dto)
        {
            var product = await _repo.Get(id);
            if (product == null) return false;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            _repo.Update(product);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
