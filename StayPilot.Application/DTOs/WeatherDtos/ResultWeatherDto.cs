namespace StayPilot.Application.DTOs.WeatherDtos
{
    public class ResultWeatherDto
    {
        public string CityName { get; set; } = string.Empty;

        public string CountryName { get; set; } = string.Empty;

        public decimal Temperature { get; set; }

        public string Condition { get; set; } = string.Empty;

        public int Humidity { get; set; }

        public decimal WindSpeed { get; set; }

        public string IconUrl { get; set; } = string.Empty;
    }
}