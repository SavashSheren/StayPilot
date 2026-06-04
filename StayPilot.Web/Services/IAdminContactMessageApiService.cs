using StayPilot.Application.DTOs.ContactDtos;

namespace StayPilot.Web.Services
{
    public interface IAdminContactMessageApiService
    {
        Task<List<ResultContactMessageDto>> GetAllAsync();

        Task<List<ResultContactMessageDto>> GetUnreadAsync();

        Task<ResultContactMessageDto?> GetByIdAsync(int id);

        Task<bool> UpdateStatusAsync(UpdateContactMessageStatusDto updateContactMessageStatusDto);

        Task<bool> DeleteAsync(int id);
    }
}