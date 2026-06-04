using StayPilot.Application.DTOs.SystemDtos;
using StayPilot.Web.Areas.Admin.ViewModels;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class AdminApiHealthService : IAdminApiHealthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AdminApiHealthService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
            _configuration = configuration;
        }

        public async Task<ApiHealthViewModel> GetSystemStatusAsync()
        {
            var model = new ApiHealthViewModel
            {
                ApiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? string.Empty
            };

            try
            {
                var apiHealth = await _httpClient.GetFromJsonAsync<ResultApiHealthDto>("api/Health");

                if (apiHealth is not null)
                {
                    model.ApiHealth = apiHealth;
                }
            }
            catch
            {
                model.ApiHealth = new ResultApiHealthDto
                {
                    Status = "Unavailable",
                    Project = "StayPilot",
                    Service = "StayPilot.API",
                    Message = "API health endpoint could not be reached.",
                    CheckedAt = DateTime.UtcNow
                };
            }

            try
            {
                var databaseHealth = await _httpClient.GetFromJsonAsync<ResultDatabaseHealthDto>("api/Health/database");

                if (databaseHealth is not null)
                {
                    model.DatabaseHealth = databaseHealth;
                }
            }
            catch
            {
                model.DatabaseHealth = new ResultDatabaseHealthDto
                {
                    Database = "StayPilotDb",
                    CanConnect = false,
                    CheckedAt = DateTime.UtcNow
                };
            }

            return model;
        }
    }
}