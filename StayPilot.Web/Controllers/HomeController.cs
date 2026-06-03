using Microsoft.AspNetCore.Mvc;
using StayPilot.Web.Services;

namespace StayPilot.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeContentApiService _homeContentApiService;

        public HomeController(IHomeContentApiService homeContentApiService)
        {
            _homeContentApiService = homeContentApiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _homeContentApiService.GetLandingContentAsync();

            return View(model);
        }
    }
}