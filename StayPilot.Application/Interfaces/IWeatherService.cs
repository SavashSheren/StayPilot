using StayPilot.Application.DTOs.WeatherDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IWeatherService
    {
        Task<ResultWeatherDto> GetCurrentWeatherAsync(string cityName);
    }
}