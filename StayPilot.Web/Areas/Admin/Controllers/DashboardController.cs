using Microsoft.AspNetCore.Mvc;
using StayPilot.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace StayPilot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IAdminDashboardApiService _adminDashboardApiService;

        public DashboardController(IAdminDashboardApiService adminDashboardApiService)
        {
            _adminDashboardApiService = adminDashboardApiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _adminDashboardApiService.GetDashboardSummaryAsync();

            return View(model);
        }
    }
}