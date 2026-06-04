namespace StayPilot.Application.DTOs.SystemDtos
{
    public class ResultApiHealthDto
    {
        public string Status { get; set; } = string.Empty;

        public string Project { get; set; } = string.Empty;

        public string Service { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime CheckedAt { get; set; }
    }
}