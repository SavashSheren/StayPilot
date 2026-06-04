using StayPilot.Application.DTOs.DestinationDtos;

namespace StayPilot.Web.Services
{
    public interface IAdminDestinationApiService
    {
        Task<List<ResultDestinationDto>> GetAllAsync();

        Task<ResultDestinationDto?> GetByIdAsync(int id);

        Task<bool> CreateAsync(CreateDestinationDto createDestinationDto);

        Task<bool> UpdateAsync(UpdateDestinationDto updateDestinationDto);

        Task<bool> DeleteAsync(int id);

        Task<bool> SetActiveStatusAsync(int id, bool isActive);
    }
}