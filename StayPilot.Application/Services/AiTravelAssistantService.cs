using StayPilot.Application.DTOs.AiDtos;
using StayPilot.Application.Interfaces;
using StayPilot.Domain.Entities;

namespace StayPilot.Application.Services
{
    public class AiTravelAssistantService : IAiTravelAssistantService
    {
        private readonly IAiProviderClient _aiProviderClient;
        private readonly IGenericRepository<AiConversationLog> _aiConversationLogRepository;

        public AiTravelAssistantService(
            IAiProviderClient aiProviderClient,
            IGenericRepository<AiConversationLog> aiConversationLogRepository)
        {
            _aiProviderClient = aiProviderClient;
            _aiConversationLogRepository = aiConversationLogRepository;
        }

        public async Task<ResultAiTravelAnswerDto> AskAsync(
            CreateAiTravelQuestionDto questionDto,
            string? userIpAddress,
            string? userAgent)
        {
            if (string.IsNullOrWhiteSpace(questionDto.Question))
            {
                return new ResultAiTravelAnswerDto
                {
                    Answer = string.Empty,
                    ModelName = "StayPilot.MockAI",
                    IsSuccessful = false,
                    ErrorMessage = "Question is required."
                };
            }

            var answer = await _aiProviderClient.GenerateTravelAnswerAsync(questionDto);

            var log = new AiConversationLog
            {
                UserQuestion = questionDto.Question,
                AiAnswer = answer.Answer,
                ModelName = answer.ModelName,
                TokenUsage = EstimateTokenUsage(questionDto.Question, answer.Answer),
                IsSuccessful = answer.IsSuccessful,
                ErrorMessage = answer.ErrorMessage,
                UserIpAddress = userIpAddress,
                UserAgent = userAgent
            };

            await _aiConversationLogRepository.CreateAsync(log);

            return answer;
        }

        private static int EstimateTokenUsage(string question, string answer)
        {
            var totalLength = question.Length + answer.Length;

            return Math.Max(1, totalLength / 4);
        }
    }
}