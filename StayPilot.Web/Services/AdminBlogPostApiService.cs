using StayPilot.Application.DTOs.BlogDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class AdminBlogPostApiService : IAdminBlogPostApiService
    {
        private readonly HttpClient _httpClient;

        public AdminBlogPostApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<List<ResultBlogPostDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResultBlogPostDto>>(
                    "api/admin/BlogPosts");

                return response ?? new List<ResultBlogPostDto>();
            }
            catch
            {
                return new List<ResultBlogPostDto>();
            }
        }

        public async Task<ResultBlogPostDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResultBlogPostDto>(
                    $"api/admin/BlogPosts/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateAsync(CreateBlogPostDto createBlogPostDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "api/admin/BlogPosts",
                    createBlogPostDto);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UpdateBlogPostDto updateBlogPostDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    "api/admin/BlogPosts",
                    updateBlogPostDto);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(
                    $"api/admin/BlogPosts/{id}");

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SetActiveStatusAsync(int id, bool isActive)
        {
            try
            {
                var response = await _httpClient.PatchAsync(
                    $"api/admin/BlogPosts/{id}/status?isActive={isActive}",
                    null);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}