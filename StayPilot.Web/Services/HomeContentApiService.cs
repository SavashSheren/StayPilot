using StayPilot.Application.DTOs.HomeDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class HomeContentApiService : IHomeContentApiService
    {
        private readonly HttpClient _httpClient;

        public HomeContentApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<ResultHomeContentDto> GetLandingContentAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResultHomeContentDto>("api/public/HomeContent/landing");

                return response ?? new ResultHomeContentDto();
            }
            catch
            {
                return new ResultHomeContentDto();
            }
        }
    }
}