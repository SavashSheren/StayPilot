namespace StayPilot.Application.DTOs.HotelDtos
{
    public class ResultHotelDto
    {
        public string HotelId { get; set; } = string.Empty;

        public string HotelName { get; set; } = string.Empty;

        public string CityName { get; set; } = string.Empty;

        public string CountryName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Currency { get; set; } = string.Empty;

        public double ReviewScore { get; set; }

        public string ReviewScoreWord { get; set; } = string.Empty;

        public string BookingUrl { get; set; } = string.Empty;
    }
}