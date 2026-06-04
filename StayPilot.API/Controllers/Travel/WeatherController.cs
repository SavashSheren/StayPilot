using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Travel
{
    [Route("api/travel/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather(string cityName = "Istanbul")
        {
            var value = await _weatherService.GetCurrentWeatherAsync(cityName);

            return Ok(value);
        }
    }
}