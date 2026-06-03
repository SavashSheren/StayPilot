using StayPilot.Application.DTOs.AdminDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class AdminDashboardApiService : IAdminDashboardApiService
    {
        private readonly HttpClient _httpClient;

        public AdminDashboardApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<ResultAdminDashboardDto> GetDashboardSummaryAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResultAdminDashboardDto>(
                    "api/admin/Dashboard/summary");

                return response ?? new ResultAdminDashboardDto();
            }
            catch
            {
                return new ResultAdminDashboardDto();
            }
        }
    }
}