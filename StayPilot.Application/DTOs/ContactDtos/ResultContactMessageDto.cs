using StayPilot.Domain.Enums;

namespace StayPilot.Application.DTOs.ContactDtos
{
    public class ResultContactMessageDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public ContactMessageStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}