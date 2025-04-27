using System.Net.Http.Json;
using Client.Services;
using Client.Components;
using Client.Models;

namespace Client.Services
{
    public class ProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Product>>("https://localhost:7085/api/Product/GetAll");
                return result ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return new List<Product>();
            }
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<Product>($"https://localhost:7085/api/Product/GetById/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching product {id}: {ex.Message}");
                return null;
            }
        }

        public async Task CreateProductAsync(Product product)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("https://localhost:7085/api/Product/Create", product);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"https://localhost:7085/api/Product/Update/{product.ProductId}", product);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product {product.ProductId}: {ex.Message}");
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"https://localhost:7085/api/Product/Delete/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product {id}: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Product>>($"https://localhost:7085/api/Product/GetAll?pageNumber={pageNumber}&pageSize={pageSize}");
                return result ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching paginated products: {ex.Message}");
                return new List<Product>();
            }
        }

    }
}
