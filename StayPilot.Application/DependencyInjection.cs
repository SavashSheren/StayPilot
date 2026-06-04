using Microsoft.Extensions.DependencyInjection;
using StayPilot.Application.Interfaces;
using StayPilot.Application.Mappings;
using StayPilot.Application.Services;

namespace StayPilot.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(GeneralMapping));

            services.AddScoped<IHeroSectionService, HeroSectionService>();
            services.AddScoped<IDestinationService, DestinationService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<IContactMessageService, ContactMessageService>();
            services.AddScoped<IHotelSearchService, HotelSearchService>();
            services.AddScoped<IAiTravelAssistantService, AiTravelAssistantService>();

            return services;
        }
    }
}