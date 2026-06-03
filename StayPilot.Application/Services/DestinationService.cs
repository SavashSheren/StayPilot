using AutoMapper;
using StayPilot.Application.DTOs.DestinationDtos;
using StayPilot.Application.Interfaces;
using StayPilot.Domain.Entities;

namespace StayPilot.Application.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IGenericRepository<Destination> _destinationRepository;
        private readonly IMapper _mapper;

        public DestinationService(
            IGenericRepository<Destination> destinationRepository,
            IMapper mapper)
        {
            _destinationRepository = destinationRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultDestinationDto>> GetAllAsync()
        {
            var destinations = await _destinationRepository.GetAllAsync();

            return _mapper.Map<List<ResultDestinationDto>>(destinations);
        }

        public async Task<List<ResultDestinationDto>> GetActiveListAsync()
        {
            var destinations = await _destinationRepository.GetActiveListAsync();

            return _mapper.Map<List<ResultDestinationDto>>(destinations);
        }

        public async Task<List<ResultDestinationDto>> GetFeaturedListAsync()
        {
            var destinations = await _destinationRepository.GetActiveListAsync();

            destinations = destinations
                .Where(x => x.IsFeatured)
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            return _mapper.Map<List<ResultDestinationDto>>(destinations);
        }

        public async Task<ResultDestinationDto?> GetByIdAsync(int id)
        {
            var destination = await _destinationRepository.GetByIdAsync(id);

            if (destination is null)
            {
                return null;
            }

            return _mapper.Map<ResultDestinationDto>(destination);
        }

        public async Task CreateAsync(CreateDestinationDto createDestinationDto)
        {
            var destination = _mapper.Map<Destination>(createDestinationDto);

            await _destinationRepository.CreateAsync(destination);
        }

        public async Task UpdateAsync(UpdateDestinationDto updateDestinationDto)
        {
            var existingDestination = await _destinationRepository.GetByIdAsync(updateDestinationDto.Id);

            if (existingDestination is null)
            {
                return;
            }

            _mapper.Map(updateDestinationDto, existingDestination);

            await _destinationRepository.UpdateAsync(existingDestination);
        }

        public async Task DeleteAsync(int id)
        {
            var destination = await _destinationRepository.GetByIdAsync(id);

            if (destination is null)
            {
                return;
            }

            await _destinationRepository.DeleteAsync(destination);
        }

        public async Task SetActiveStatusAsync(int id, bool isActive)
        {
            await _destinationRepository.SetActiveStatusAsync(id, isActive);
        }
    }
}