using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StayPilot.Domain.Entities;
using StayPilot.Infrastructure.Context;

namespace StayPilot.Infrastructure.SeedData
{
    public static class DefaultDataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<StayPilotDbContext>();

            await context.Database.MigrateAsync();

            await SeedHeroSectionsAsync(context);
            await SeedDestinationsAsync(context);
            await SeedBlogPostsAsync(context);
        }

        private static async Task SeedHeroSectionsAsync(StayPilotDbContext context)
        {
            if (await context.HeroSections.AnyAsync())
            {
                return;
            }

            var heroSection = new HeroSection
            {
                Title = "Find Your Perfect Stay with AI",
                Subtitle = "Smart Hotel Discovery",
                Description = "StayPilot helps travelers discover hotels, compare destinations and plan smarter trips with AI-powered travel intelligence.",
                BackgroundImageUrl = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e",
                PrimaryButtonText = "Search Hotels",
                PrimaryButtonUrl = "/hotels",
                SecondaryButtonText = "Ask AI Assistant",
                SecondaryButtonUrl = "/ai-travel-assistant",
                DisplayOrder = 1,
                IsActive = true
            };

            await context.HeroSections.AddAsync(heroSection);
            await context.SaveChangesAsync();
        }

        private static async Task SeedDestinationsAsync(StayPilotDbContext context)
        {
            if (await context.Destinations.AnyAsync())
            {
                return;
            }

            var destinations = new List<Destination>
            {
                new()
                {
                    CityName = "Istanbul",
                    CountryName = "Türkiye",
                    Description = "A timeless city where history, culture and modern hospitality meet across two continents.",
                    ImageUrl = "https://images.unsplash.com/photo-1524231757912-21f4fe3a7200",
                    HighlightText = "Best for culture, food and city breaks",
                    AverageHotelPrice = 125,
                    DisplayOrder = 1,
                    IsFeatured = true,
                    IsActive = true
                },
                new()
                {
                    CityName = "Paris",
                    CountryName = "France",
                    Description = "Iconic architecture, romantic streets and world-class hotels for unforgettable city escapes.",
                    ImageUrl = "https://images.unsplash.com/photo-1502602898657-3e91760cbb34",
                    HighlightText = "Best for romance and luxury stays",
                    AverageHotelPrice = 210,
                    DisplayOrder = 2,
                    IsFeatured = true,
                    IsActive = true
                },
                new()
                {
                    CityName = "Dubai",
                    CountryName = "United Arab Emirates",
                    Description = "A futuristic travel destination with luxury resorts, skyline views and premium hospitality.",
                    ImageUrl = "https://images.unsplash.com/photo-1512453979798-5ea266f8880c",
                    HighlightText = "Best for luxury and modern travel",
                    AverageHotelPrice = 240,
                    DisplayOrder = 3,
                    IsFeatured = true,
                    IsActive = true
                },
                new()
                {
                    CityName = "London",
                    CountryName = "United Kingdom",
                    Description = "A global capital of history, culture, business travel and premium hotel experiences.",
                    ImageUrl = "https://images.unsplash.com/photo-1513635269975-59663e0ac1ad",
                    HighlightText = "Best for business and culture",
                    AverageHotelPrice = 195,
                    DisplayOrder = 4,
                    IsFeatured = true,
                    IsActive = true
                },
                new()
                {
                    CityName = "Rome",
                    CountryName = "Italy",
                    Description = "Ancient landmarks, charming streets and boutique hotels in the heart of Italian culture.",
                    ImageUrl = "https://images.unsplash.com/photo-1529260830199-42c24126f198",
                    HighlightText = "Best for history and food lovers",
                    AverageHotelPrice = 165,
                    DisplayOrder = 5,
                    IsFeatured = true,
                    IsActive = true
                },
                new()
                {
                    CityName = "Barcelona",
                    CountryName = "Spain",
                    Description = "Mediterranean energy, architecture, beaches and stylish hotels for modern travelers.",
                    ImageUrl = "https://images.unsplash.com/photo-1539037116277-4db20889f2d4",
                    HighlightText = "Best for design and seaside city breaks",
                    AverageHotelPrice = 175,
                    DisplayOrder = 6,
                    IsFeatured = true,
                    IsActive = true
                }
            };

            await context.Destinations.AddRangeAsync(destinations);
            await context.SaveChangesAsync();
        }

        private static async Task SeedBlogPostsAsync(StayPilotDbContext context)
        {
            if (await context.BlogPosts.AnyAsync())
            {
                return;
            }

            var blogPosts = new List<BlogPost>
            {
                new()
                {
                    Title = "How AI Is Changing Hotel Discovery",
                    Slug = "how-ai-is-changing-hotel-discovery",
                    Summary = "AI is making travel planning faster, smarter and more personalized for modern travelers.",
                    Content = "AI-powered travel platforms help users compare hotels, understand destinations and make better booking decisions with personalized recommendations.",
                    CoverImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945",
                    AuthorName = "StayPilot Editorial",
                    ReadingTimeMinute = 4,
                    IsFeatured = true,
                    PublishedDate = DateTime.UtcNow,
                    IsActive = true
                },
                new()
                {
                    Title = "Top Features Travelers Expect from Modern Hotel Platforms",
                    Slug = "top-features-travelers-expect-from-modern-hotel-platforms",
                    Summary = "Modern travelers expect speed, trust, personalization and clear destination intelligence.",
                    Content = "A strong hotel discovery platform should combine fast search, clean filters, trusted reviews, destination insights and transparent pricing.",
                    CoverImageUrl = "https://images.unsplash.com/photo-1556745757-8d76bdb6984b",
                    AuthorName = "StayPilot Editorial",
                    ReadingTimeMinute = 5,
                    IsFeatured = true,
                    PublishedDate = DateTime.UtcNow.AddDays(-2),
                    IsActive = true
                },
                new()
                {
                    Title = "Why Destination Intelligence Matters Before Booking",
                    Slug = "why-destination-intelligence-matters-before-booking",
                    Summary = "Hotel choice is not only about price; travelers also need weather, location and local context.",
                    Content = "Destination intelligence helps users understand the best time to travel, expected weather, average prices and local travel conditions before booking.",
                    CoverImageUrl = "https://images.unsplash.com/photo-1488646953014-85cb44e25828",
                    AuthorName = "StayPilot Editorial",
                    ReadingTimeMinute = 3,
                    IsFeatured = true,
                    PublishedDate = DateTime.UtcNow.AddDays(-5),
                    IsActive = true
                }
            };

            await context.BlogPosts.AddRangeAsync(blogPosts);
            await context.SaveChangesAsync();
        }
    }
}