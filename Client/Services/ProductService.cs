using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Product>> GetProductsAsync()
            => await _http.GetFromJsonAsync<List<Product>>("products");

        public async Task<Product> GetProductByIdAsync(int id)
            => await _http.GetFromJsonAsync<Product>($"products/{id}");

        public async Task AddProductAsync(Product product)
            => await _http.PostAsJsonAsync("products", product);

        public async Task UpdateProductAsync(Product product)
            => await _http.PutAsJsonAsync($"products/{product.Id}", product);

        public async Task DeleteProductAsync(int id)
            => await _http.DeleteAsync($"products/{id}");
    }
}
