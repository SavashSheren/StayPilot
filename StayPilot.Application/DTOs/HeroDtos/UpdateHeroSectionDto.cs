namespace StayPilot.Application.DTOs.HeroDtos
{
    public class UpdateHeroSectionDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Subtitle { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string BackgroundImageUrl { get; set; } = string.Empty;

        public string PrimaryButtonText { get; set; } = string.Empty;

        public string PrimaryButtonUrl { get; set; } = string.Empty;

        public string SecondaryButtonText { get; set; } = string.Empty;

        public string SecondaryButtonUrl { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}