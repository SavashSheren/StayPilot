using StayPilot.Application.DTOs.AiDtos;

namespace StayPilot.Web.Services
{
    public interface IAdminAiConversationLogApiService
    {
        Task<List<ResultAiConversationLogDto>> GetAllAsync();

        Task<ResultAiConversationLogDto?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);
    }
}