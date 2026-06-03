using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.HomeDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Public
{
    [Route("api/public/[controller]")]
    [ApiController]
    public class HomeContentController : ControllerBase
    {
        private readonly IHeroSectionService _heroSectionService;
        private readonly IDestinationService _destinationService;
        private readonly IBlogPostService _blogPostService;

        public HomeContentController(
            IHeroSectionService heroSectionService,
            IDestinationService destinationService,
            IBlogPostService blogPostService)
        {
            _heroSectionService = heroSectionService;
            _destinationService = destinationService;
            _blogPostService = blogPostService;
        }

        [HttpGet("hero")]
        public async Task<IActionResult> GetHeroSection()
        {
            var heroSections = await _heroSectionService.GetActiveListAsync();

            var heroSection = heroSections
                .OrderBy(x => x.DisplayOrder)
                .FirstOrDefault();

            if (heroSection is null)
            {
                return NotFound(new
                {
                    message = "Active hero section not found."
                });
            }

            return Ok(heroSection);
        }

        [HttpGet("destinations/featured")]
        public async Task<IActionResult> GetFeaturedDestinations()
        {
            var values = await _destinationService.GetFeaturedListAsync();

            return Ok(values);
        }

        [HttpGet("blog-posts/featured")]
        public async Task<IActionResult> GetFeaturedBlogPosts()
        {
            var values = await _blogPostService.GetFeaturedListAsync();

            return Ok(values);
        }

        [HttpGet("landing")]
        public async Task<IActionResult> GetLandingContent()
        {
            var heroSections = await _heroSectionService.GetActiveListAsync();

            var heroSection = heroSections
                .OrderBy(x => x.DisplayOrder)
                .FirstOrDefault();

            var featuredDestinations = await _destinationService.GetFeaturedListAsync();
            var featuredBlogPosts = await _blogPostService.GetFeaturedListAsync();

            var result = new ResultHomeContentDto
            {
                HeroSection = heroSection,
                FeaturedDestinations = featuredDestinations,
                FeaturedBlogPosts = featuredBlogPosts
            };

            return Ok(result);
        }
    }
}