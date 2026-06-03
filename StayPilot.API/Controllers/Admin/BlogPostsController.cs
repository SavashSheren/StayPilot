using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.BlogDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var values = await _blogPostService.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveBlogPosts()
        {
            var values = await _blogPostService.GetActiveListAsync();

            return Ok(values);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedBlogPosts()
        {
            var values = await _blogPostService.GetFeaturedListAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPostById(int id)
        {
            var value = await _blogPostService.GetByIdAsync(id);

            if (value is null)
            {
                return NotFound(new
                {
                    message = "Blog post not found."
                });
            }

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostDto createBlogPostDto)
        {
            await _blogPostService.CreateAsync(createBlogPostDto);

            return Ok(new
            {
                message = "Blog post created successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlogPost(UpdateBlogPostDto updateBlogPostDto)
        {
            var existingBlogPost = await _blogPostService.GetByIdAsync(updateBlogPostDto.Id);

            if (existingBlogPost is null)
            {
                return NotFound(new
                {
                    message = "Blog post not found."
                });
            }

            await _blogPostService.UpdateAsync(updateBlogPostDto);

            return Ok(new
            {
                message = "Blog post updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var existingBlogPost = await _blogPostService.GetByIdAsync(id);

            if (existingBlogPost is null)
            {
                return NotFound(new
                {
                    message = "Blog post not found."
                });
            }

            await _blogPostService.DeleteAsync(id);

            return Ok(new
            {
                message = "Blog post deleted successfully."
            });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> SetBlogPostStatus(int id, bool isActive)
        {
            var existingBlogPost = await _blogPostService.GetByIdAsync(id);

            if (existingBlogPost is null)
            {
                return NotFound(new
                {
                    message = "Blog post not found."
                });
            }

            await _blogPostService.SetActiveStatusAsync(id, isActive);

            return Ok(new
            {
                message = isActive
                    ? "Blog post activated successfully."
                    : "Blog post deactivated successfully."
            });
        }
    }
}