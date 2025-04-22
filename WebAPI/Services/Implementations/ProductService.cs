using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Services.Abstractions;

namespace WebAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IMapper _mapper;
        public ProductService(IProductRepository productRepository,IMapper mapper )
        {
            this._productRepository = productRepository;
            this._mapper = mapper;
        }
        public async Task<Product> AddAsync(ProductDto productDto)
        {
            var entity = _mapper.Map<Product>(productDto);
              _productRepository.Add(entity);
            await  _productRepository.SaveChangesAsync();
            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var entities =await _productRepository.GetAllAsync();
            var products = _mapper.Map<List<ProductDto>>(entities);
            return products;
        }

        public async Task<ProductDto?> GetById(int id)
        {
            ProductDto productdto = null;
            var product = await _productRepository.Get(id);
            if (product != null)
            {
                 productdto = _mapper.Map<ProductDto>(product);
            }
            return productdto;
        }

        public void Update(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
