using StayPilot.Domain.Common;
using StayPilot.Domain.Enums;

namespace StayPilot.Domain.Entities
{
    public class ApiRequestLog : BaseEntity
    {
        public string ProviderName { get; set; } = string.Empty;

        public string Endpoint { get; set; } = string.Empty;

        public string RequestPath { get; set; } = string.Empty;

        public ApiRequestStatus Status { get; set; }

        public int StatusCode { get; set; }

        public long DurationInMilliseconds { get; set; }

        public string? ErrorMessage { get; set; }
    }
}