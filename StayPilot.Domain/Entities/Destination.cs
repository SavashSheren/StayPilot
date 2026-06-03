using StayPilot.Domain.Common;

namespace StayPilot.Domain.Entities
{
    public class Destination : BaseEntity
    {
        public string CityName { get; set; } = string.Empty;

        public string CountryName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string HighlightText { get; set; } = string.Empty;

        public decimal AverageHotelPrice { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsFeatured { get; set; } = true;
    }
}