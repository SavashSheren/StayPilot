using StayPilot.Application.DTOs.CurrencyDtos;
using StayPilot.Application.DTOs.WeatherDtos;

namespace StayPilot.Web.Services
{
    public interface ITravelIntelligenceApiService
    {
        Task<ResultWeatherDto> GetWeatherAsync(string cityName);

        Task<ResultCurrencyRateDto> GetCurrencyRateAsync(string baseCurrency, string targetCurrency);
    }
}