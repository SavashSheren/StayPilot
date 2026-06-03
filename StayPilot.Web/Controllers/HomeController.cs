using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.ContactDtos;
using StayPilot.Web.Services;

namespace StayPilot.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeContentApiService _homeContentApiService;
        private readonly IContactMessageApiService _contactMessageApiService;

        public HomeController(
            IHomeContentApiService homeContentApiService,
            IContactMessageApiService contactMessageApiService)
        {
            _homeContentApiService = homeContentApiService;
            _contactMessageApiService = contactMessageApiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _homeContentApiService.GetLandingContentAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendContactMessage(CreateContactMessageDto createContactMessageDto)
        {
            if (string.IsNullOrWhiteSpace(createContactMessageDto.FullName) ||
                string.IsNullOrWhiteSpace(createContactMessageDto.Email) ||
                string.IsNullOrWhiteSpace(createContactMessageDto.Subject) ||
                string.IsNullOrWhiteSpace(createContactMessageDto.Message))
            {
                TempData["ContactError"] = "Please fill in all required fields.";

                return Redirect($"{Url.Action(nameof(Index), "Home")}#contact");
            }

            var isSuccess = await _contactMessageApiService.SendContactMessageAsync(createContactMessageDto);

            if (!isSuccess)
            {
                TempData["ContactError"] = "Your message could not be sent. Please try again.";

                return Redirect($"{Url.Action(nameof(Index), "Home")}#contact");
            }

            TempData["ContactSuccess"] = "Your message has been sent successfully. StayPilot team will review it soon.";

            return Redirect($"{Url.Action(nameof(Index), "Home")}#contact");
        }
    }
}