using StayPilot.Application.DTOs.WeatherDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.Infrastructure.ExternalServices
{
    public class MockWeatherProviderClient : IWeatherProviderClient
    {
        public Task<ResultWeatherDto> GetCurrentWeatherAsync(string cityName)
        {
            var normalizedCity = cityName.Trim();

            var result = normalizedCity.ToLowerInvariant() switch
            {
                "paris" => new ResultWeatherDto
                {
                    CityName = "Paris",
                    CountryName = "France",
                    Temperature = 18,
                    Condition = "Partly Cloudy",
                    Humidity = 62,
                    WindSpeed = 12,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/1163/1163661.png"
                },

                "dubai" => new ResultWeatherDto
                {
                    CityName = "Dubai",
                    CountryName = "United Arab Emirates",
                    Temperature = 34,
                    Condition = "Sunny",
                    Humidity = 48,
                    WindSpeed = 9,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/869/869869.png"
                },

                "london" => new ResultWeatherDto
                {
                    CityName = "London",
                    CountryName = "United Kingdom",
                    Temperature = 15,
                    Condition = "Light Rain",
                    Humidity = 74,
                    WindSpeed = 16,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/1163/1163657.png"
                },

                _ => new ResultWeatherDto
                {
                    CityName = normalizedCity,
                    CountryName = "Türkiye",
                    Temperature = 22,
                    Condition = "Clear Sky",
                    Humidity = 58,
                    WindSpeed = 10,
                    IconUrl = "https://cdn-icons-png.flaticon.com/512/869/869869.png"
                }
            };

            return Task.FromResult(result);
        }
    }
}