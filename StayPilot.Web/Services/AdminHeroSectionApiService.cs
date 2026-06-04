using StayPilot.Application.DTOs.HeroDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class AdminHeroSectionApiService : IAdminHeroSectionApiService
    {
        private readonly HttpClient _httpClient;

        public AdminHeroSectionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<List<ResultHeroSectionDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResultHeroSectionDto>>(
                    "api/admin/HeroSections");

                return response ?? new List<ResultHeroSectionDto>();
            }
            catch
            {
                return new List<ResultHeroSectionDto>();
            }
        }

        public async Task<ResultHeroSectionDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResultHeroSectionDto>(
                    $"api/admin/HeroSections/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateAsync(CreateHeroSectionDto createHeroSectionDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "api/admin/HeroSections",
                    createHeroSectionDto);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UpdateHeroSectionDto updateHeroSectionDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    "api/admin/HeroSections",
                    updateHeroSectionDto);

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
                    $"api/admin/HeroSections/{id}");

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
                    $"api/admin/HeroSections/{id}/status?isActive={isActive}",
                    JsonContent.Create(new { }));

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}