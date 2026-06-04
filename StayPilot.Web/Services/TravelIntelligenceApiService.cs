using StayPilot.Application.DTOs.CurrencyDtos;
using StayPilot.Application.DTOs.WeatherDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class TravelIntelligenceApiService : ITravelIntelligenceApiService
    {
        private readonly HttpClient _httpClient;

        public TravelIntelligenceApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<ResultWeatherDto> GetWeatherAsync(string cityName)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResultWeatherDto>(
                    $"api/travel/Weather/current?cityName={Uri.EscapeDataString(cityName)}");

                return response ?? new ResultWeatherDto();
            }
            catch
            {
                return new ResultWeatherDto
                {
                    CityName = cityName,
                    CountryName = "Unknown",
                    Temperature = 0,
                    Condition = "Unavailable",
                    Humidity = 0,
                    WindSpeed = 0,
                    IconUrl = string.Empty
                };
            }
        }

        public async Task<ResultCurrencyRateDto> GetCurrencyRateAsync(string baseCurrency, string targetCurrency)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResultCurrencyRateDto>(
                    $"api/travel/Currency/rate?baseCurrency={baseCurrency}&targetCurrency={targetCurrency}");

                return response ?? new ResultCurrencyRateDto();
            }
            catch
            {
                return new ResultCurrencyRateDto
                {
                    BaseCurrency = baseCurrency,
                    TargetCurrency = targetCurrency,
                    Rate = 0,
                    UpdatedDate = DateTime.UtcNow
                };
            }
        }
    }
}