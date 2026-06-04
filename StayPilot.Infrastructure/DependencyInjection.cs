using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StayPilot.Application.Interfaces;
using StayPilot.Infrastructure.Context;
using StayPilot.Infrastructure.Repositories;
using StayPilot.Infrastructure.ExternalServices;

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
            services.AddScoped<IHotelProviderClient, MockHotelProviderClient>();
            services.AddScoped<IAiProviderClient, MockAiProviderClient>();
            services.AddScoped<IWeatherProviderClient, MockWeatherProviderClient>();
            services.AddScoped<ICurrencyProviderClient, MockCurrencyProviderClient>();

            return services;
        }
    }
}