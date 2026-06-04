using Microsoft.AspNetCore.Mvc;
using StayPilot.Web.Services;

namespace StayPilot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApiHealthController : Controller
    {
        private readonly IAdminApiHealthService _adminApiHealthService;

        public ApiHealthController(IAdminApiHealthService adminApiHealthService)
        {
            _adminApiHealthService = adminApiHealthService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _adminApiHealthService.GetSystemStatusAsync();

            return View(model);
        }
    }
}