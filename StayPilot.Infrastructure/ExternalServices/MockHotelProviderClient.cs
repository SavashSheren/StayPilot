using StayPilot.Application.DTOs.HotelDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.Infrastructure.ExternalServices
{
    public class MockHotelProviderClient : IHotelProviderClient
    {
        public Task<List<ResultHotelDto>> SearchHotelsAsync(HotelSearchRequestDto requestDto)
        {
            var destination = string.IsNullOrWhiteSpace(requestDto.Destination)
                ? "Istanbul"
                : requestDto.Destination.Trim();

            var hotels = new List<ResultHotelDto>
            {
                new()
                {
                    HotelId = "SP-IST-001",
                    HotelName = "StayPilot Grand Bosphorus",
                    CityName = destination,
                    CountryName = "Türkiye",
                    Address = "Bosphorus View District, Istanbul",
                    ImageUrl = "https://images.unsplash.com/photo-1566073771259-6a8506099945",
                    Price = 185,
                    Currency = "USD",
                    ReviewScore = 9.2,
                    ReviewScoreWord = "Wonderful",
                    BookingUrl = "https://www.booking.com"
                },
                new()
                {
                    HotelId = "SP-IST-002",
                    HotelName = "Nexus Urban Hotel",
                    CityName = destination,
                    CountryName = "Türkiye",
                    Address = "City Center, Istanbul",
                    ImageUrl = "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa",
                    Price = 142,
                    Currency = "USD",
                    ReviewScore = 8.8,
                    ReviewScoreWord = "Excellent",
                    BookingUrl = "https://www.booking.com"
                },
                new()
                {
                    HotelId = "SP-IST-003",
                    HotelName = "Pilot Suites Airport",
                    CityName = destination,
                    CountryName = "Türkiye",
                    Address = "Airport Business Zone",
                    ImageUrl = "https://images.unsplash.com/photo-1582719508461-905c673771fd",
                    Price = 118,
                    Currency = "USD",
                    ReviewScore = 8.5,
                    ReviewScoreWord = "Very Good",
                    BookingUrl = "https://www.booking.com"
                },
                new()
                {
                    HotelId = "SP-IST-004",
                    HotelName = "Azure Stay Collection",
                    CityName = destination,
                    CountryName = "Türkiye",
                    Address = "Premium Coastal Area",
                    ImageUrl = "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb",
                    Price = 225,
                    Currency = "USD",
                    ReviewScore = 9.4,
                    ReviewScoreWord = "Exceptional",
                    BookingUrl = "https://www.booking.com"
                },
                new()
                {
                    HotelId = "SP-IST-005",
                    HotelName = "Metroline Business Hotel",
                    CityName = destination,
                    CountryName = "Türkiye",
                    Address = "Business District",
                    ImageUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427",
                    Price = 96,
                    Currency = "USD",
                    ReviewScore = 8.1,
                    ReviewScoreWord = "Very Good",
                    BookingUrl = "https://www.booking.com"
                }
            };

            if (requestDto.MinPrice.HasValue)
            {
                hotels = hotels
                    .Where(x => x.Price >= requestDto.MinPrice.Value)
                    .ToList();
            }

            if (requestDto.MaxPrice.HasValue)
            {
                hotels = hotels
                    .Where(x => x.Price <= requestDto.MaxPrice.Value)
                    .ToList();
            }

            hotels = hotels
                .OrderByDescending(x => x.ReviewScore)
                .ToList();

            return Task.FromResult(hotels);
        }

        public Task<ResultHotelDetailDto?> GetHotelDetailAsync(string hotelId)
        {
            var hotel = new ResultHotelDetailDto
            {
                HotelId = hotelId,
                HotelName = hotelId switch
                {
                    "SP-IST-001" => "StayPilot Grand Bosphorus",
                    "SP-IST-002" => "Nexus Urban Hotel",
                    "SP-IST-003" => "Pilot Suites Airport",
                    "SP-IST-004" => "Azure Stay Collection",
                    "SP-IST-005" => "Metroline Business Hotel",
                    _ => "StayPilot Signature Hotel"
                },
                Description = "A premium hotel experience designed for modern travelers who value comfort, location intelligence and smart booking decisions.",
                CityName = "Istanbul",
                CountryName = "Türkiye",
                Address = "Central Istanbul, Türkiye",
                Latitude = 41.0082m,
                Longitude = 28.9784m,
                Price = 185,
                Currency = "USD",
                ReviewScore = 9.1,
                ReviewScoreWord = "Wonderful",
                BookingUrl = "https://www.booking.com",
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1566073771259-6a8506099945",
                    "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa",
                    "https://images.unsplash.com/photo-1582719508461-905c673771fd",
                    "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb"
                },
                Facilities = new List<string>
                {
                    "Free WiFi",
                    "Airport Transfer",
                    "Breakfast Included",
                    "Fitness Center",
                    "City View",
                    "24/7 Reception",
                    "Business Lounge"
                }
            };

            return Task.FromResult<ResultHotelDetailDto?>(hotel);
        }
    }
}