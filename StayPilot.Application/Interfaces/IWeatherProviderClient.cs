using StayPilot.Application.DTOs.WeatherDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IWeatherProviderClient
    {
        Task<ResultWeatherDto> GetCurrentWeatherAsync(string cityName);
    }
}