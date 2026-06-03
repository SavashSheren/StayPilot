namespace StayPilot.Application.DTOs.AiDtos
{
    public class CreateAiTravelQuestionDto
    {
        public string Question { get; set; } = string.Empty;

        public string? Destination { get; set; }

        public string? TravelDate { get; set; }

        public string? TravelStyle { get; set; }
    }
}