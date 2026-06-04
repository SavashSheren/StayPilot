using StayPilot.Application.DTOs.SystemDtos;

namespace StayPilot.Web.Areas.Admin.ViewModels
{
    public class ApiHealthViewModel
    {
        public ResultApiHealthDto ApiHealth { get; set; } = new();

        public ResultDatabaseHealthDto DatabaseHealth { get; set; } = new();

        public string ApiBaseUrl { get; set; } = string.Empty;

        public bool IsApiHealthy => ApiHealth.Status.Equals("Healthy", StringComparison.OrdinalIgnoreCase);

        public bool IsDatabaseHealthy => DatabaseHealth.CanConnect;
    }
}