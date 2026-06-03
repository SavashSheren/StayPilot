using StayPilot.Domain.Enums;

namespace StayPilot.Application.DTOs.ContactDtos
{
    public class UpdateContactMessageStatusDto
    {
        public int Id { get; set; }

        public ContactMessageStatus Status { get; set; }
    }
}