using StayPilot.Application.DTOs.AiDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IAiTravelAssistantService
    {
        Task<ResultAiTravelAnswerDto> AskAsync(
            CreateAiTravelQuestionDto questionDto,
            string? userIpAddress,
            string? userAgent);
    }
}