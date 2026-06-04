using StayPilot.Application.DTOs.HotelDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class HotelSearchApiService : IHotelSearchApiService
    {
        private readonly HttpClient _httpClient;

        public HotelSearchApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<List<ResultHotelDto>> SearchHotelsAsync(HotelSearchRequestDto requestDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "api/Hotels/search",
                    requestDto);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<ResultHotelDto>();
                }

                var hotels = await response.Content.ReadFromJsonAsync<List<ResultHotelDto>>();

                return hotels ?? new List<ResultHotelDto>();
            }
            catch
            {
                return new List<ResultHotelDto>();
            }
        }

        public async Task<ResultHotelDetailDto?> GetHotelDetailAsync(string hotelId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResultHotelDetailDto>(
                    $"api/Hotels/{hotelId}");
            }
            catch
            {
                return null;
            }
        }
    }
}