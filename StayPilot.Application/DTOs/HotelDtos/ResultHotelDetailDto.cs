namespace StayPilot.Application.DTOs.HotelDtos
{
    public class ResultHotelDetailDto
    {
        public string HotelId { get; set; } = string.Empty;

        public string HotelName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string CityName { get; set; } = string.Empty;

        public string CountryName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; } = string.Empty;

        public double ReviewScore { get; set; }

        public string ReviewScoreWord { get; set; } = string.Empty;

        public List<string> ImageUrls { get; set; } = new();

        public List<string> Facilities { get; set; } = new();

        public string BookingUrl { get; set; } = string.Empty;
    }
}