using StayPilot.Application.DTOs.WeatherDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherProviderClient _weatherProviderClient;

        public WeatherService(IWeatherProviderClient weatherProviderClient)
        {
            _weatherProviderClient = weatherProviderClient;
        }

        public async Task<ResultWeatherDto> GetCurrentWeatherAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                cityName = "Istanbul";
            }

            return await _weatherProviderClient.GetCurrentWeatherAsync(cityName);
        }
    }
}