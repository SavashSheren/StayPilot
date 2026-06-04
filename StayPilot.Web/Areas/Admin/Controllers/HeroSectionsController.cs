using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.HeroDtos;
using StayPilot.Web.Services;

namespace StayPilot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroSectionsController : Controller
    {
        private readonly IAdminHeroSectionApiService _adminHeroSectionApiService;

        public HeroSectionsController(IAdminHeroSectionApiService adminHeroSectionApiService)
        {
            _adminHeroSectionApiService = adminHeroSectionApiService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _adminHeroSectionApiService.GetAllAsync();

            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateHeroSectionDto
            {
                Title = "Find Your Perfect Stay with AI",
                Subtitle = "Smart Hotel Discovery",
                Description = "StayPilot helps travelers discover hotels, compare destinations and plan smarter trips with AI-powered travel intelligence.",
                BackgroundImageUrl = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e",
                PrimaryButtonText = "Search Hotels",
                PrimaryButtonUrl = "#search",
                SecondaryButtonText = "Ask AI Assistant",
                SecondaryButtonUrl = "#ai",
                DisplayOrder = 1
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHeroSectionDto createHeroSectionDto)
        {
            if (string.IsNullOrWhiteSpace(createHeroSectionDto.Title) ||
                string.IsNullOrWhiteSpace(createHeroSectionDto.Description) ||
                string.IsNullOrWhiteSpace(createHeroSectionDto.BackgroundImageUrl))
            {
                TempData["AdminError"] = "Title, description and background image URL are required.";

                return View(createHeroSectionDto);
            }

            var result = await _adminHeroSectionApiService.CreateAsync(createHeroSectionDto);

            if (!result)
            {
                TempData["AdminError"] = "Hero section could not be created.";

                return View(createHeroSectionDto);
            }

            TempData["AdminSuccess"] = "Hero section created successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var heroSection = await _adminHeroSectionApiService.GetByIdAsync(id);

            if (heroSection is null)
            {
                TempData["AdminError"] = "Hero section not found.";

                return RedirectToAction(nameof(Index));
            }

            var model = new UpdateHeroSectionDto
            {
                Id = heroSection.Id,
                Title = heroSection.Title,
                Subtitle = heroSection.Subtitle,
                Description = heroSection.Description,
                BackgroundImageUrl = heroSection.BackgroundImageUrl,
                PrimaryButtonText = heroSection.PrimaryButtonText,
                PrimaryButtonUrl = heroSection.PrimaryButtonUrl,
                SecondaryButtonText = heroSection.SecondaryButtonText,
                SecondaryButtonUrl = heroSection.SecondaryButtonUrl,
                DisplayOrder = heroSection.DisplayOrder,
                IsActive = heroSection.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateHeroSectionDto updateHeroSectionDto)
        {
            if (string.IsNullOrWhiteSpace(updateHeroSectionDto.Title) ||
                string.IsNullOrWhiteSpace(updateHeroSectionDto.Description) ||
                string.IsNullOrWhiteSpace(updateHeroSectionDto.BackgroundImageUrl))
            {
                TempData["AdminError"] = "Title, description and background image URL are required.";

                return View(updateHeroSectionDto);
            }

            var result = await _adminHeroSectionApiService.UpdateAsync(updateHeroSectionDto);

            if (!result)
            {
                TempData["AdminError"] = "Hero section could not be updated.";

                return View(updateHeroSectionDto);
            }

            TempData["AdminSuccess"] = "Hero section updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(int id)
        {
            var result = await _adminHeroSectionApiService.SetActiveStatusAsync(id, true);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Hero section activated successfully."
                : "Hero section could not be activated.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _adminHeroSectionApiService.SetActiveStatusAsync(id, false);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Hero section deactivated successfully."
                : "Hero section could not be deactivated.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminHeroSectionApiService.DeleteAsync(id);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Hero section deleted successfully."
                : "Hero section could not be deleted.";

            return RedirectToAction(nameof(Index));
        }
    }
}