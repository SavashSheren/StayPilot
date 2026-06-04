using StayPilot.Application.DTOs.AiDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class AdminAiConversationLogApiService : IAdminAiConversationLogApiService
    {
        private readonly HttpClient _httpClient;

        public AdminAiConversationLogApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<List<ResultAiConversationLogDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResultAiConversationLogDto>>(
                    "api/admin/AiConversationLogs");

                return response ?? new List<ResultAiConversationLogDto>();
            }
            catch
            {
                return new List<ResultAiConversationLogDto>();
            }
        }

        public async Task<ResultAiConversationLogDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResultAiConversationLogDto>(
                    $"api/admin/AiConversationLogs/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(
                    $"api/admin/AiConversationLogs/{id}");

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}