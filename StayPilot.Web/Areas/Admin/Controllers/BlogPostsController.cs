using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.BlogDtos;
using StayPilot.Web.Services;
using System.Text.RegularExpressions;

namespace StayPilot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly IAdminBlogPostApiService _adminBlogPostApiService;

        public BlogPostsController(IAdminBlogPostApiService adminBlogPostApiService)
        {
            _adminBlogPostApiService = adminBlogPostApiService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _adminBlogPostApiService.GetAllAsync();

            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateBlogPostDto
            {
                Title = "Smart Travel Planning with AI",
                Slug = "",
                Summary = "Discover how AI-powered travel platforms help users make faster and smarter hotel decisions.",
                Content = "AI-powered travel planning helps users compare destinations, understand hotel options and make better decisions before booking.",
                CoverImageUrl = "https://images.unsplash.com/photo-1488646953014-85cb44e25828",
                AuthorName = "StayPilot Editorial",
                ReadingTimeMinute = 4,
                IsFeatured = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBlogPostDto createBlogPostDto)
        {
            if (string.IsNullOrWhiteSpace(createBlogPostDto.Title) ||
                string.IsNullOrWhiteSpace(createBlogPostDto.Summary) ||
                string.IsNullOrWhiteSpace(createBlogPostDto.Content) ||
                string.IsNullOrWhiteSpace(createBlogPostDto.CoverImageUrl) ||
                string.IsNullOrWhiteSpace(createBlogPostDto.AuthorName))
            {
                TempData["AdminError"] = "Title, summary, content, cover image and author are required.";

                return View(createBlogPostDto);
            }

            if (string.IsNullOrWhiteSpace(createBlogPostDto.Slug))
            {
                createBlogPostDto.Slug = GenerateSlug(createBlogPostDto.Title);
            }

            var result = await _adminBlogPostApiService.CreateAsync(createBlogPostDto);

            if (!result)
            {
                TempData["AdminError"] = "Blog post could not be created. Slug may already exist.";

                return View(createBlogPostDto);
            }

            TempData["AdminSuccess"] = "Blog post created successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var blogPost = await _adminBlogPostApiService.GetByIdAsync(id);

            if (blogPost is null)
            {
                TempData["AdminError"] = "Blog post not found.";

                return RedirectToAction(nameof(Index));
            }

            var model = new UpdateBlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Slug = blogPost.Slug,
                Summary = blogPost.Summary,
                Content = blogPost.Content,
                CoverImageUrl = blogPost.CoverImageUrl,
                AuthorName = blogPost.AuthorName,
                ReadingTimeMinute = blogPost.ReadingTimeMinute,
                IsFeatured = blogPost.IsFeatured,
                IsActive = blogPost.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateBlogPostDto updateBlogPostDto)
        {
            if (string.IsNullOrWhiteSpace(updateBlogPostDto.Title) ||
                string.IsNullOrWhiteSpace(updateBlogPostDto.Slug) ||
                string.IsNullOrWhiteSpace(updateBlogPostDto.Summary) ||
                string.IsNullOrWhiteSpace(updateBlogPostDto.Content) ||
                string.IsNullOrWhiteSpace(updateBlogPostDto.CoverImageUrl) ||
                string.IsNullOrWhiteSpace(updateBlogPostDto.AuthorName))
            {
                TempData["AdminError"] = "Title, slug, summary, content, cover image and author are required.";

                return View(updateBlogPostDto);
            }

            var result = await _adminBlogPostApiService.UpdateAsync(updateBlogPostDto);

            if (!result)
            {
                TempData["AdminError"] = "Blog post could not be updated. Slug may already exist.";

                return View(updateBlogPostDto);
            }

            TempData["AdminSuccess"] = "Blog post updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(int id)
        {
            var result = await _adminBlogPostApiService.SetActiveStatusAsync(id, true);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Blog post activated successfully."
                : "Blog post could not be activated.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _adminBlogPostApiService.SetActiveStatusAsync(id, false);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Blog post deactivated successfully."
                : "Blog post could not be deactivated.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminBlogPostApiService.DeleteAsync(id);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Blog post deleted successfully."
                : "Blog post could not be deleted.";

            return RedirectToAction(nameof(Index));
        }

        private static string GenerateSlug(string title)
        {
            var slug = title.Trim().ToLowerInvariant();

            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"-+", "-");

            return $"{slug}-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
    }
}