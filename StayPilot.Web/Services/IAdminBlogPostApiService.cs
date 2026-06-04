using StayPilot.Application.DTOs.BlogDtos;

namespace StayPilot.Web.Services
{
    public interface IAdminBlogPostApiService
    {
        Task<List<ResultBlogPostDto>> GetAllAsync();

        Task<ResultBlogPostDto?> GetByIdAsync(int id);

        Task<bool> CreateAsync(CreateBlogPostDto createBlogPostDto);

        Task<bool> UpdateAsync(UpdateBlogPostDto updateBlogPostDto);

        Task<bool> DeleteAsync(int id);

        Task<bool> SetActiveStatusAsync(int id, bool isActive);
    }
}