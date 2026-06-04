using StayPilot.Application.DTOs.HeroDtos;

namespace StayPilot.Web.Services
{
    public interface IAdminHeroSectionApiService
    {
        Task<List<ResultHeroSectionDto>> GetAllAsync();

        Task<ResultHeroSectionDto?> GetByIdAsync(int id);

        Task<bool> CreateAsync(CreateHeroSectionDto createHeroSectionDto);

        Task<bool> UpdateAsync(UpdateHeroSectionDto updateHeroSectionDto);

        Task<bool> DeleteAsync(int id);

        Task<bool> SetActiveStatusAsync(int id, bool isActive);
    }
}