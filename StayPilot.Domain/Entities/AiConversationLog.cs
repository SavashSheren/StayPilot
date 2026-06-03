using StayPilot.Domain.Common;

namespace StayPilot.Domain.Entities
{
    public class AiConversationLog : BaseEntity
    {
        public string UserQuestion { get; set; } = string.Empty;

        public string AiAnswer { get; set; } = string.Empty;

        public string? UserIpAddress { get; set; }

        public string? UserAgent { get; set; }

        public string ModelName { get; set; } = string.Empty;

        public int TokenUsage { get; set; }

        public bool IsSuccessful { get; set; } = true;

        public string? ErrorMessage { get; set; }
    }
}