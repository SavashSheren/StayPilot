namespace StayPilot.Application.DTOs.AiDtos
{
    public class ResultAiConversationLogDto
    {
        public int Id { get; set; }

        public string UserQuestion { get; set; } = string.Empty;

        public string AiAnswer { get; set; } = string.Empty;

        public string ModelName { get; set; } = string.Empty;

        public int TokenUsage { get; set; }

        public bool IsSuccessful { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
