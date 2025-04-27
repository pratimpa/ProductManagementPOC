using System.Net.Http.Json;
using Client.Services;
using Client.Components;
using Client.Models;
namespace Client.Services
{
    public class CategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<Category>>("https://localhost:7085/api/Category/GetAll");
                return result ?? new List<Category>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching categories: {ex.Message}");
                return new List<Category>(); // <-- Important: return empty list, NOT crash
            }
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<Category>($"https://localhost:7085/api/Category/GetById/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching category {id}: {ex.Message}");
                return null; // gracefully return null
            }
        }

        public async Task CreateCategoryAsync(Category category)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("https://localhost:7085/api/Category/Create", category);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating category: {ex.Message}");
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"https://localhost:7085/api/Category/Update/{category.CategoryId}", category);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating category {category.CategoryId}: {ex.Message}");
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"https://localhost:7085/api/Category/Delete/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category {id}: {ex.Message}");
            }
        }
    }
}


