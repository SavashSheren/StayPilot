using StayPilot.Application.DTOs.AiDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.Infrastructure.ExternalServices
{
    public class MockAiProviderClient : IAiProviderClient
    {
        public Task<ResultAiTravelAnswerDto> GenerateTravelAnswerAsync(CreateAiTravelQuestionDto questionDto)
        {
            var destination = string.IsNullOrWhiteSpace(questionDto.Destination)
                ? "your destination"
                : questionDto.Destination.Trim();

            var travelStyle = string.IsNullOrWhiteSpace(questionDto.TravelStyle)
                ? "balanced"
                : questionDto.TravelStyle.Trim();

            var travelDate = string.IsNullOrWhiteSpace(questionDto.TravelDate)
                ? "your selected dates"
                : questionDto.TravelDate.Trim();

            var answer =
$@"Here is a smart StayPilot recommendation for {destination}.

Travel context:
- Destination: {destination}
- Travel date: {travelDate}
- Travel style: {travelStyle}

Recommendation:
1. Choose your hotel based on location first, price second. A cheap hotel far from your main activities usually costs more in time and transport.
2. For a {travelStyle.ToLower()} trip, compare at least 3 districts before booking.
3. Check weather and local mobility before finalizing the hotel.
4. Prefer hotels with strong review scores, clear location advantages and flexible cancellation.
5. If this is a short trip, stay closer to the main attraction area. If it is a business trip, stay closer to transport hubs.

StayPilot insight:
The best booking decision is not always the cheapest option. The best decision is the hotel that balances location, comfort, travel purpose and total trip cost.";

            return Task.FromResult(new ResultAiTravelAnswerDto
            {
                Answer = answer,
                ModelName = "StayPilot.MockAI",
                IsSuccessful = true,
                ErrorMessage = null
            });
        }
    }
}