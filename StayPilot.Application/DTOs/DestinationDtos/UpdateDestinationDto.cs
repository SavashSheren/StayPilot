namespace StayPilot.Application.DTOs.DestinationDtos
{
    public class UpdateDestinationDto
    {
        public int Id { get; set; }

        public string CityName { get; set; } = string.Empty;

        public string CountryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string HighlightText { get; set; } = string.Empty;

        public decimal AverageHotelPrice { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsActive { get; set; }
    }
}