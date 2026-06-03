using StayPilot.Application.DTOs.ContactDtos;

namespace StayPilot.Application.DTOs.AdminDtos
{
    public class ResultAdminDashboardDto
    {
        public int TotalHeroSections { get; set; }

        public int ActiveHeroSections { get; set; }

        public int TotalDestinations { get; set; }

        public int ActiveDestinations { get; set; }

        public int FeaturedDestinations { get; set; }

        public int TotalBlogPosts { get; set; }

        public int ActiveBlogPosts { get; set; }

        public int FeaturedBlogPosts { get; set; }

        public int TotalContactMessages { get; set; }

        public int UnreadContactMessages { get; set; }

        public List<ResultContactMessageDto> RecentContactMessages { get; set; } = new();

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
}