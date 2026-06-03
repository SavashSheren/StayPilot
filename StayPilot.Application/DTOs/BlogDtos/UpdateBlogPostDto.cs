namespace StayPilot.Application.DTOs.BlogDtos
{
    public class UpdateBlogPostDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string CoverImageUrl { get; set; } = string.Empty;

        public string AuthorName { get; set; } = string.Empty;

        public int ReadingTimeMinute { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsActive { get; set; }
    }
}