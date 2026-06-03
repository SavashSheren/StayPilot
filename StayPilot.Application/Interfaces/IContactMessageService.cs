using StayPilot.Application.DTOs.ContactDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IContactMessageService
    {
        Task<List<ResultContactMessageDto>> GetAllAsync();

        Task<List<ResultContactMessageDto>> GetUnreadListAsync();

        Task<ResultContactMessageDto?> GetByIdAsync(int id);

        Task CreateAsync(CreateContactMessageDto createContactMessageDto);

        Task UpdateStatusAsync(UpdateContactMessageStatusDto updateContactMessageStatusDto);

        Task DeleteAsync(int id);
    }
}