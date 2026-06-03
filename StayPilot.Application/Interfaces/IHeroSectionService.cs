using StayPilot.Application.DTOs.HeroDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IHeroSectionService
    {
        Task<List<ResultHeroSectionDto>> GetAllAsync();

        Task<List<ResultHeroSectionDto>> GetActiveListAsync();

        Task<ResultHeroSectionDto?> GetByIdAsync(int id);

        Task CreateAsync(CreateHeroSectionDto createHeroSectionDto);

        Task UpdateAsync(UpdateHeroSectionDto updateHeroSectionDto);

        Task DeleteAsync(int id);

        Task SetActiveStatusAsync(int id, bool isActive);
    }
}