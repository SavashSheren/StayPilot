using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StayPilot.Web.Services;

namespace StayPilot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AiConversationLogsController : Controller
    {
        private readonly IAdminAiConversationLogApiService _adminAiConversationLogApiService;

        public AiConversationLogsController(IAdminAiConversationLogApiService adminAiConversationLogApiService)
        {
            _adminAiConversationLogApiService = adminAiConversationLogApiService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _adminAiConversationLogApiService.GetAllAsync();

            return View(values);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var value = await _adminAiConversationLogApiService.GetByIdAsync(id);

            if (value is null)
            {
                TempData["AdminError"] = "AI conversation log not found.";

                return RedirectToAction(nameof(Index));
            }

            return View(value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminAiConversationLogApiService.DeleteAsync(id);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "AI conversation log deleted successfully."
                : "AI conversation log could not be deleted.";

            return RedirectToAction(nameof(Index));
        }
    }
}