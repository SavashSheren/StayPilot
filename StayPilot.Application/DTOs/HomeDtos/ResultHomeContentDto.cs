using StayPilot.Application.DTOs.BlogDtos;
using StayPilot.Application.DTOs.DestinationDtos;
using StayPilot.Application.DTOs.HeroDtos;

namespace StayPilot.Application.DTOs.HomeDtos
{
    public class ResultHomeContentDto
    {
        public ResultHeroSectionDto? HeroSection { get; set; }

        public List<ResultDestinationDto> FeaturedDestinations { get; set; } = new();

        public List<ResultBlogPostDto> FeaturedBlogPosts { get; set; } = new();
    }
}