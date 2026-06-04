using AutoMapper;
using StayPilot.Application.DTOs.AiDtos;
using StayPilot.Application.Interfaces;
using StayPilot.Domain.Entities;

namespace StayPilot.Application.Services
{
    public class AiConversationLogService : IAiConversationLogService
    {
        private readonly IGenericRepository<AiConversationLog> _aiConversationLogRepository;
        private readonly IMapper _mapper;

        public AiConversationLogService(
            IGenericRepository<AiConversationLog> aiConversationLogRepository,
            IMapper mapper)
        {
            _aiConversationLogRepository = aiConversationLogRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultAiConversationLogDto>> GetAllAsync()
        {
            var logs = await _aiConversationLogRepository.GetAllAsync();

            return _mapper.Map<List<ResultAiConversationLogDto>>(logs);
        }

        public async Task<ResultAiConversationLogDto?> GetByIdAsync(int id)
        {
            var log = await _aiConversationLogRepository.GetByIdAsync(id);

            if (log is null)
            {
                return null;
            }

            return _mapper.Map<ResultAiConversationLogDto>(log);
        }

        public async Task DeleteAsync(int id)
        {
            var log = await _aiConversationLogRepository.GetByIdAsync(id);

            if (log is null)
            {
                return;
            }

            await _aiConversationLogRepository.DeleteAsync(log);
        }
    }
}