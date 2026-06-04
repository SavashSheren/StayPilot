using Microsoft.AspNetCore.Mvc;
using StayPilot.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace StayPilot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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