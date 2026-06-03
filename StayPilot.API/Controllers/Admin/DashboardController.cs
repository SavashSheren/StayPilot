using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.AdminDtos;
using StayPilot.Application.Interfaces;
using StayPilot.Domain.Enums;

namespace StayPilot.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IHeroSectionService _heroSectionService;
        private readonly IDestinationService _destinationService;
        private readonly IBlogPostService _blogPostService;
        private readonly IContactMessageService _contactMessageService;

        public DashboardController(
            IHeroSectionService heroSectionService,
            IDestinationService destinationService,
            IBlogPostService blogPostService,
            IContactMessageService contactMessageService)
        {
            _heroSectionService = heroSectionService;
            _destinationService = destinationService;
            _blogPostService = blogPostService;
            _contactMessageService = contactMessageService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var heroSections = await _heroSectionService.GetAllAsync();
            var destinations = await _destinationService.GetAllAsync();
            var blogPosts = await _blogPostService.GetAllAsync();
            var contactMessages = await _contactMessageService.GetAllAsync();

            var result = new ResultAdminDashboardDto
            {
                TotalHeroSections = heroSections.Count,
                ActiveHeroSections = heroSections.Count(x => x.IsActive),

                TotalDestinations = destinations.Count,
                ActiveDestinations = destinations.Count(x => x.IsActive),
                FeaturedDestinations = destinations.Count(x => x.IsFeatured),

                TotalBlogPosts = blogPosts.Count,
                ActiveBlogPosts = blogPosts.Count(x => x.IsActive),
                FeaturedBlogPosts = blogPosts.Count(x => x.IsFeatured),

                TotalContactMessages = contactMessages.Count,
                UnreadContactMessages = contactMessages.Count(x => x.Status == ContactMessageStatus.Unread),

                RecentContactMessages = contactMessages
                    .OrderByDescending(x => x.CreatedDate)
                    .Take(5)
                    .ToList(),

                GeneratedAt = DateTime.UtcNow
            };

            return Ok(result);
        }
    }
}