using AutoMapper;
using StayPilot.Application.DTOs.HeroDtos;
using StayPilot.Application.Interfaces;
using StayPilot.Domain.Entities;

namespace StayPilot.Application.Services
{
    public class HeroSectionService : IHeroSectionService
    {
        private readonly IGenericRepository<HeroSection> _heroSectionRepository;
        private readonly IMapper _mapper;

        public HeroSectionService(
            IGenericRepository<HeroSection> heroSectionRepository,
            IMapper mapper)
        {
            _heroSectionRepository = heroSectionRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultHeroSectionDto>> GetAllAsync()
        {
            var heroSections = await _heroSectionRepository.GetAllAsync();

            return _mapper.Map<List<ResultHeroSectionDto>>(heroSections);
        }

        public async Task<List<ResultHeroSectionDto>> GetActiveListAsync()
        {
            var heroSections = await _heroSectionRepository.GetActiveListAsync();

            return _mapper.Map<List<ResultHeroSectionDto>>(heroSections);
        }

        public async Task<ResultHeroSectionDto?> GetByIdAsync(int id)
        {
            var heroSection = await _heroSectionRepository.GetByIdAsync(id);

            if (heroSection is null)
            {
                return null;
            }

            return _mapper.Map<ResultHeroSectionDto>(heroSection);
        }

        public async Task CreateAsync(CreateHeroSectionDto createHeroSectionDto)
        {
            var heroSection = _mapper.Map<HeroSection>(createHeroSectionDto);

            await _heroSectionRepository.CreateAsync(heroSection);
        }

        public async Task UpdateAsync(UpdateHeroSectionDto updateHeroSectionDto)
        {
            var existingHeroSection = await _heroSectionRepository.GetByIdAsync(updateHeroSectionDto.Id);

            if (existingHeroSection is null)
            {
                return;
            }

            _mapper.Map(updateHeroSectionDto, existingHeroSection);

            await _heroSectionRepository.UpdateAsync(existingHeroSection);
        }

        public async Task DeleteAsync(int id)
        {
            var heroSection = await _heroSectionRepository.GetByIdAsync(id);

            if (heroSection is null)
            {
                return;
            }

            await _heroSectionRepository.DeleteAsync(heroSection);
        }

        public async Task SetActiveStatusAsync(int id, bool isActive)
        {
            await _heroSectionRepository.SetActiveStatusAsync(id, isActive);
        }
    }
}