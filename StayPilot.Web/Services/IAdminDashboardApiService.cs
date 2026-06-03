using StayPilot.Application.DTOs.AdminDtos;

namespace StayPilot.Web.Services
{
    public interface IAdminDashboardApiService
    {
        Task<ResultAdminDashboardDto> GetDashboardSummaryAsync();
    }
}