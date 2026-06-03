using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.ContactDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Public
{
    [Route("api/public/[controller]")]
    [ApiController]
    public class ContactMessagesController : ControllerBase
    {
        private readonly IContactMessageService _contactMessageService;

        public ContactMessagesController(IContactMessageService contactMessageService)
        {
            _contactMessageService = contactMessageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactMessage(CreateContactMessageDto createContactMessageDto)
        {
            await _contactMessageService.CreateAsync(createContactMessageDto);

            return Ok(new
            {
                message = "Your message has been received successfully."
            });
        }
    }
}