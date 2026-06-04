using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.DestinationDtos;
using StayPilot.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace StayPilot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DestinationsController : Controller
    {
        private readonly IAdminDestinationApiService _adminDestinationApiService;

        public DestinationsController(IAdminDestinationApiService adminDestinationApiService)
        {
            _adminDestinationApiService = adminDestinationApiService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _adminDestinationApiService.GetAllAsync();

            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateDestinationDto
            {
                CityName = "Istanbul",
                CountryName = "Türkiye",
                Description = "A timeless city where history, culture and modern hospitality meet across two continents.",
                ImageUrl = "https://images.unsplash.com/photo-1524231757912-21f4fe3a7200",
                HighlightText = "Best for culture, food and city breaks",
                AverageHotelPrice = 125,
                DisplayOrder = 1,
                IsFeatured = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDestinationDto createDestinationDto)
        {
            if (string.IsNullOrWhiteSpace(createDestinationDto.CityName) ||
                string.IsNullOrWhiteSpace(createDestinationDto.CountryName) ||
                string.IsNullOrWhiteSpace(createDestinationDto.Description) ||
                string.IsNullOrWhiteSpace(createDestinationDto.ImageUrl))
            {
                TempData["AdminError"] = "City, country, description and image URL are required.";

                return View(createDestinationDto);
            }

            var result = await _adminDestinationApiService.CreateAsync(createDestinationDto);

            if (!result)
            {
                TempData["AdminError"] = "Destination could not be created.";

                return View(createDestinationDto);
            }

            TempData["AdminSuccess"] = "Destination created successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var destination = await _adminDestinationApiService.GetByIdAsync(id);

            if (destination is null)
            {
                TempData["AdminError"] = "Destination not found.";

                return RedirectToAction(nameof(Index));
            }

            var model = new UpdateDestinationDto
            {
                Id = destination.Id,
                CityName = destination.CityName,
                CountryName = destination.CountryName,
                Description = destination.Description,
                ImageUrl = destination.ImageUrl,
                HighlightText = destination.HighlightText,
                AverageHotelPrice = destination.AverageHotelPrice,
                DisplayOrder = destination.DisplayOrder,
                IsFeatured = destination.IsFeatured,
                IsActive = destination.IsActive
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateDestinationDto updateDestinationDto)
        {
            if (string.IsNullOrWhiteSpace(updateDestinationDto.CityName) ||
                string.IsNullOrWhiteSpace(updateDestinationDto.CountryName) ||
                string.IsNullOrWhiteSpace(updateDestinationDto.Description) ||
                string.IsNullOrWhiteSpace(updateDestinationDto.ImageUrl))
            {
                TempData["AdminError"] = "City, country, description and image URL are required.";

                return View(updateDestinationDto);
            }

            var result = await _adminDestinationApiService.UpdateAsync(updateDestinationDto);

            if (!result)
            {
                TempData["AdminError"] = "Destination could not be updated.";

                return View(updateDestinationDto);
            }

            TempData["AdminSuccess"] = "Destination updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(int id)
        {
            var result = await _adminDestinationApiService.SetActiveStatusAsync(id, true);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Destination activated successfully."
                : "Destination could not be activated.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _adminDestinationApiService.SetActiveStatusAsync(id, false);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Destination deactivated successfully."
                : "Destination could not be deactivated.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminDestinationApiService.DeleteAsync(id);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Destination deleted successfully."
                : "Destination could not be deleted.";

            return RedirectToAction(nameof(Index));
        }
    }
}