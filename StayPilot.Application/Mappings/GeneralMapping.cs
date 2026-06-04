using AutoMapper;
using StayPilot.Application.DTOs.BlogDtos;
using StayPilot.Application.DTOs.ContactDtos;
using StayPilot.Application.DTOs.DestinationDtos;
using StayPilot.Application.DTOs.HeroDtos;
using StayPilot.Domain.Entities;
using StayPilot.Application.DTOs.AiDtos;

namespace StayPilot.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<HeroSection, ResultHeroSectionDto>().ReverseMap();
            CreateMap<CreateHeroSectionDto, HeroSection>();
            CreateMap<UpdateHeroSectionDto, HeroSection>();

            CreateMap<Destination, ResultDestinationDto>().ReverseMap();
            CreateMap<CreateDestinationDto, Destination>();
            CreateMap<UpdateDestinationDto, Destination>();

            CreateMap<BlogPost, ResultBlogPostDto>().ReverseMap();
            CreateMap<CreateBlogPostDto, BlogPost>();
            CreateMap<UpdateBlogPostDto, BlogPost>();

            CreateMap<ContactMessage, ResultContactMessageDto>().ReverseMap();
            CreateMap<CreateContactMessageDto, ContactMessage>();

            CreateMap<AiConversationLog, ResultAiConversationLogDto>().ReverseMap();
        }
    }
}