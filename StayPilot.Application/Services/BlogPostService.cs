using AutoMapper;
using StayPilot.Application.DTOs.BlogDtos;
using StayPilot.Application.Interfaces;
using StayPilot.Domain.Entities;

namespace StayPilot.Application.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IGenericRepository<BlogPost> _blogPostRepository;
        private readonly IMapper _mapper;

        public BlogPostService(
            IGenericRepository<BlogPost> blogPostRepository,
            IMapper mapper)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultBlogPostDto>> GetAllAsync()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();

            return _mapper.Map<List<ResultBlogPostDto>>(blogPosts);
        }

        public async Task<List<ResultBlogPostDto>> GetActiveListAsync()
        {
            var blogPosts = await _blogPostRepository.GetActiveListAsync();

            return _mapper.Map<List<ResultBlogPostDto>>(blogPosts);
        }

        public async Task<List<ResultBlogPostDto>> GetFeaturedListAsync()
        {
            var blogPosts = await _blogPostRepository.GetActiveListAsync();

            blogPosts = blogPosts
                .Where(x => x.IsFeatured)
                .OrderByDescending(x => x.PublishedDate)
                .ToList();

            return _mapper.Map<List<ResultBlogPostDto>>(blogPosts);
        }

        public async Task<ResultBlogPostDto?> GetByIdAsync(int id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);

            if (blogPost is null)
            {
                return null;
            }

            return _mapper.Map<ResultBlogPostDto>(blogPost);
        }

        public async Task CreateAsync(CreateBlogPostDto createBlogPostDto)
        {
            var blogPost = _mapper.Map<BlogPost>(createBlogPostDto);

            blogPost.PublishedDate = DateTime.UtcNow;

            await _blogPostRepository.CreateAsync(blogPost);
        }

        public async Task UpdateAsync(UpdateBlogPostDto updateBlogPostDto)
        {
            var existingBlogPost = await _blogPostRepository.GetByIdAsync(updateBlogPostDto.Id);

            if (existingBlogPost is null)
            {
                return;
            }

            _mapper.Map(updateBlogPostDto, existingBlogPost);

            await _blogPostRepository.UpdateAsync(existingBlogPost);
        }

        public async Task DeleteAsync(int id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);

            if (blogPost is null)
            {
                return;
            }

            await _blogPostRepository.DeleteAsync(blogPost);
        }

        public async Task SetActiveStatusAsync(int id, bool isActive)
        {
            await _blogPostRepository.SetActiveStatusAsync(id, isActive);
        }
    }
}