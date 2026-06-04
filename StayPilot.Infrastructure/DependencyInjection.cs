using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StayPilot.Application.Interfaces;
using StayPilot.Infrastructure.Context;
using StayPilot.Infrastructure.ExternalServices;
using StayPilot.Infrastructure.Repositories;

namespace StayPilot.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StayPilotDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddHttpClient<RapidApiBookingHotelProviderClient>();

            var providerName = configuration["HotelProvider:ProviderName"];
            var rapidApiKey = configuration["HotelProvider:RapidApiKey"];

            if (string.Equals(providerName, "RapidApiBooking", StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(rapidApiKey))
            {
                services.AddScoped<IHotelProviderClient, RapidApiBookingHotelProviderClient>();
            }
            else
            {
                services.AddScoped<IHotelProviderClient, MockHotelProviderClient>();
            }

            services.AddScoped<IAiProviderClient, MockAiProviderClient>();
            services.AddScoped<IWeatherProviderClient, MockWeatherProviderClient>();
            services.AddScoped<ICurrencyProviderClient, MockCurrencyProviderClient>();

            return services;
        }
    }
}