using StayPilot.Application.DTOs.AiDtos;

namespace StayPilot.Web.ViewModels
{
    public class AiAssistantPageViewModel
    {
        public CreateAiTravelQuestionDto QuestionForm { get; set; } = new();

        public ResultAiTravelAnswerDto? Answer { get; set; }

        public bool HasAnswer { get; set; }

        public string? ErrorMessage { get; set; }
    }
}