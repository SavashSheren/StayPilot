using StayPilot.Application.DTOs.DestinationDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class AdminDestinationApiService : IAdminDestinationApiService
    {
        private readonly HttpClient _httpClient;

        public AdminDestinationApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<List<ResultDestinationDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResultDestinationDto>>(
                    "api/admin/Destinations");

                return response ?? new List<ResultDestinationDto>();
            }
            catch
            {
                return new List<ResultDestinationDto>();
            }
        }

        public async Task<ResultDestinationDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResultDestinationDto>(
                    $"api/admin/Destinations/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateAsync(CreateDestinationDto createDestinationDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "api/admin/Destinations",
                    createDestinationDto);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UpdateDestinationDto updateDestinationDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    "api/admin/Destinations",
                    updateDestinationDto);

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
                    $"api/admin/Destinations/{id}");

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
                    $"api/admin/Destinations/{id}/status?isActive={isActive}",
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