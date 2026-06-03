using AutoMapper;
using StayPilot.Application.DTOs.ContactDtos;
using StayPilot.Application.Interfaces;
using StayPilot.Domain.Entities;
using StayPilot.Domain.Enums;

namespace StayPilot.Application.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IGenericRepository<ContactMessage> _contactMessageRepository;
        private readonly IMapper _mapper;

        public ContactMessageService(
            IGenericRepository<ContactMessage> contactMessageRepository,
            IMapper mapper)
        {
            _contactMessageRepository = contactMessageRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultContactMessageDto>> GetAllAsync()
        {
            var contactMessages = await _contactMessageRepository.GetAllAsync();

            return _mapper.Map<List<ResultContactMessageDto>>(contactMessages);
        }

        public async Task<List<ResultContactMessageDto>> GetUnreadListAsync()
        {
            var contactMessages = await _contactMessageRepository.GetActiveListAsync();

            contactMessages = contactMessages
                .Where(x => x.Status == ContactMessageStatus.Unread)
                .OrderByDescending(x => x.Id)
                .ToList();

            return _mapper.Map<List<ResultContactMessageDto>>(contactMessages);
        }

        public async Task<ResultContactMessageDto?> GetByIdAsync(int id)
        {
            var contactMessage = await _contactMessageRepository.GetByIdAsync(id);

            if (contactMessage is null)
            {
                return null;
            }

            return _mapper.Map<ResultContactMessageDto>(contactMessage);
        }

        public async Task CreateAsync(CreateContactMessageDto createContactMessageDto)
        {
            var contactMessage = _mapper.Map<ContactMessage>(createContactMessageDto);

            contactMessage.Status = ContactMessageStatus.Unread;

            await _contactMessageRepository.CreateAsync(contactMessage);
        }

        public async Task UpdateStatusAsync(UpdateContactMessageStatusDto updateContactMessageStatusDto)
        {
            var existingContactMessage = await _contactMessageRepository.GetByIdAsync(updateContactMessageStatusDto.Id);

            if (existingContactMessage is null)
            {
                return;
            }

            existingContactMessage.Status = updateContactMessageStatusDto.Status;
            existingContactMessage.UpdatedDate = DateTime.UtcNow;

            await _contactMessageRepository.UpdateAsync(existingContactMessage);
        }

        public async Task DeleteAsync(int id)
        {
            var contactMessage = await _contactMessageRepository.GetByIdAsync(id);

            if (contactMessage is null)
            {
                return;
            }

            await _contactMessageRepository.DeleteAsync(contactMessage);
        }
    }
}