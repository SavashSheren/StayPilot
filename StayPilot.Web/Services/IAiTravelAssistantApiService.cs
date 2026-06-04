using StayPilot.Application.DTOs.AiDtos;

namespace StayPilot.Web.Services
{
    public interface IAiTravelAssistantApiService
    {
        Task<ResultAiTravelAnswerDto?> AskAsync(CreateAiTravelQuestionDto questionDto);
    }
}