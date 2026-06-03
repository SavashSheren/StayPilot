using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.ContactDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ContactMessagesController : ControllerBase
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactMessagesController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContactMessages()
        {
            var values = await _contactMessageService.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadContactMessages()
        {
            var values = await _contactMessageService.GetUnreadListAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactMessageById(int id)
        {
            var value = await _contactMessageService.GetByIdAsync(id);

            if (value is null)
            {
                return NotFound(new
                {
                    message = "Contact message not found."
                });
            }

            return Ok(value);
        }

        [HttpPatch("status")]
        public async Task<IActionResult> UpdateContactMessageStatus(UpdateContactMessageStatusDto updateContactMessageStatusDto)
        {
            var existingContactMessage = await _contactMessageService.GetByIdAsync(updateContactMessageStatusDto.Id);

            if (existingContactMessage is null)
            {
                return NotFound(new
                {
                    message = "Contact message not found."
                });
            }

            await _contactMessageService.UpdateStatusAsync(updateContactMessageStatusDto);

            return Ok(new
            {
                message = "Contact message status updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactMessage(int id)
        {
            var existingContactMessage = await _contactMessageService.GetByIdAsync(id);

            if (existingContactMessage is null)
            {
                return NotFound(new
                {
                    message = "Contact message not found."
                });
            }

            await _contactMessageService.DeleteAsync(id);

            return Ok(new
            {
                message = "Contact message deleted successfully."
            });
        }
    }
}