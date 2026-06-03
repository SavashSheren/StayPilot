using StayPilot.Application.DTOs.DestinationDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IDestinationService
    {
        Task<List<ResultDestinationDto>> GetAllAsync();

        Task<List<ResultDestinationDto>> GetActiveListAsync();

        Task<List<ResultDestinationDto>> GetFeaturedListAsync();

        Task<ResultDestinationDto?> GetByIdAsync(int id);

        Task CreateAsync(CreateDestinationDto createDestinationDto);

        Task UpdateAsync(UpdateDestinationDto updateDestinationDto);

        Task DeleteAsync(int id);

        Task SetActiveStatusAsync(int id, bool isActive);
    }
}