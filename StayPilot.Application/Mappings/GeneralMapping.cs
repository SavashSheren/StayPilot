using AutoMapper;
using StayPilot.Application.DTOs.HeroDtos;
using StayPilot.Domain.Entities;

namespace StayPilot.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<HeroSection, ResultHeroSectionDto>().ReverseMap();
            CreateMap<CreateHeroSectionDto, HeroSection>();
            CreateMap<UpdateHeroSectionDto, HeroSection>();
        }
    }
}