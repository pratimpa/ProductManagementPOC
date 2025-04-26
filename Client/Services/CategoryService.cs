using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Category>> GetCategoriesAsync()
            => await _http.GetFromJsonAsync<List<Category>>("categories");

        public async Task<Category> GetCategoryByIdAsync(int id)
            => await _http.GetFromJsonAsync<Category>($"categories/{id}");

        public async Task AddCategoryAsync(Category category)
            => await _http.PostAsJsonAsync("categories", category);

        public async Task UpdateCategoryAsync(Category category)
            => await _http.PutAsJsonAsync($"categories/{category.Id}", category);

        public async Task DeleteCategoryAsync(int id)
            => await _http.DeleteAsync($"categories/{id}");
    }
}
