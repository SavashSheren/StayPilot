namespace StayPilot.Application.DTOs.SystemDtos
{
    public class ResultDatabaseHealthDto
    {
        public string Database { get; set; } = string.Empty;

        public bool CanConnect { get; set; }

        public DateTime CheckedAt { get; set; }
    }
}