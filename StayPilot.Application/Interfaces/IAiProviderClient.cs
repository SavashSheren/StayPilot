using StayPilot.Application.DTOs.AiDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IAiProviderClient
    {
        Task<ResultAiTravelAnswerDto> GenerateTravelAnswerAsync(CreateAiTravelQuestionDto questionDto);
    }
}