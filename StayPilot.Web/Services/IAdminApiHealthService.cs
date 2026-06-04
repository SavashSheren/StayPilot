using StayPilot.Web.Areas.Admin.ViewModels;

namespace StayPilot.Web.Services
{
    public interface IAdminApiHealthService
    {
        Task<ApiHealthViewModel> GetSystemStatusAsync();
    }
}