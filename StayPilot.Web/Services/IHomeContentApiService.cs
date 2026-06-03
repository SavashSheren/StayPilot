using StayPilot.Application.DTOs.HomeDtos;

namespace StayPilot.Web.Services
{
    public interface IHomeContentApiService
    {
        Task<ResultHomeContentDto> GetLandingContentAsync();
    }
}