using StayPilot.Application.DTOs.ContactDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class AdminContactMessageApiService : IAdminContactMessageApiService
    {
        private readonly HttpClient _httpClient;

        public AdminContactMessageApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<List<ResultContactMessageDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResultContactMessageDto>>(
                    "api/admin/ContactMessages");

                return response ?? new List<ResultContactMessageDto>();
            }
            catch
            {
                return new List<ResultContactMessageDto>();
            }
        }

        public async Task<List<ResultContactMessageDto>> GetUnreadAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResultContactMessageDto>>(
                    "api/admin/ContactMessages/unread");

                return response ?? new List<ResultContactMessageDto>();
            }
            catch
            {
                return new List<ResultContactMessageDto>();
            }
        }

        public async Task<ResultContactMessageDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResultContactMessageDto>(
                    $"api/admin/ContactMessages/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateStatusAsync(UpdateContactMessageStatusDto updateContactMessageStatusDto)
        {
            try
            {
                var response = await _httpClient.PatchAsJsonAsync(
                    "api/admin/ContactMessages/status",
                    updateContactMessageStatusDto);

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
                var response = await _httpClient.DeleteAsync($"api/admin/ContactMessages/{id}");

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}