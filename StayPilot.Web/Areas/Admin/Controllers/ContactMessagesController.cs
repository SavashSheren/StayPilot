using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.ContactDtos;
using StayPilot.Domain.Enums;
using StayPilot.Web.Services;

namespace StayPilot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactMessagesController : Controller
    {
        private readonly IAdminContactMessageApiService _adminContactMessageApiService;

        public ContactMessagesController(IAdminContactMessageApiService adminContactMessageApiService)
        {
            _adminContactMessageApiService = adminContactMessageApiService;
        }

        public async Task<IActionResult> Index(string filter = "all")
        {
            var messages = filter.ToLower() == "unread"
                ? await _adminContactMessageApiService.GetUnreadAsync()
                : await _adminContactMessageApiService.GetAllAsync();

            ViewBag.Filter = filter;

            return View(messages);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var message = await _adminContactMessageApiService.GetByIdAsync(id);

            if (message is null)
            {
                TempData["AdminError"] = "Contact message not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _adminContactMessageApiService.UpdateStatusAsync(new UpdateContactMessageStatusDto
            {
                Id = id,
                Status = ContactMessageStatus.Read
            });

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Message marked as read."
                : "Message status could not be updated.";

            return RedirectToAction(nameof(Detail), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Archive(int id)
        {
            var result = await _adminContactMessageApiService.UpdateStatusAsync(new UpdateContactMessageStatusDto
            {
                Id = id,
                Status = ContactMessageStatus.Archived
            });

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Message archived successfully."
                : "Message could not be archived.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminContactMessageApiService.DeleteAsync(id);

            TempData[result ? "AdminSuccess" : "AdminError"] = result
                ? "Message deleted successfully."
                : "Message could not be deleted.";

            return RedirectToAction(nameof(Index));
        }
    }
}