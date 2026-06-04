using StayPilot.Application.DTOs.AiDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IAiConversationLogService
    {
        Task<List<ResultAiConversationLogDto>> GetAllAsync();

        Task<ResultAiConversationLogDto?> GetByIdAsync(int id);

        Task DeleteAsync(int id);
    }
}