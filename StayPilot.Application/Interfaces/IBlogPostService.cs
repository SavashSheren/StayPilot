using StayPilot.Application.DTOs.BlogDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task<List<ResultBlogPostDto>> GetAllAsync();

        Task<List<ResultBlogPostDto>> GetActiveListAsync();

        Task<List<ResultBlogPostDto>> GetFeaturedListAsync();

        Task<ResultBlogPostDto?> GetByIdAsync(int id);

        Task CreateAsync(CreateBlogPostDto createBlogPostDto);

        Task UpdateAsync(UpdateBlogPostDto updateBlogPostDto);

        Task DeleteAsync(int id);

        Task SetActiveStatusAsync(int id, bool isActive);
    }
}