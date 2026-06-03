namespace StayPilot.Application.DTOs.AiDtos
{
    public class ResultAiTravelAnswerDto
    {
        public string Answer { get; set; } = string.Empty;

        public string ModelName { get; set; } = string.Empty;

        public bool IsSuccessful { get; set; }

        public string? ErrorMessage { get; set; }
    }
}