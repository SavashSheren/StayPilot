using StayPilot.Application.DTOs.ContactDtos;

namespace StayPilot.Web.Services
{
    public interface IContactMessageApiService
    {
        Task<bool> SendContactMessageAsync(CreateContactMessageDto createContactMessageDto);
    }
}