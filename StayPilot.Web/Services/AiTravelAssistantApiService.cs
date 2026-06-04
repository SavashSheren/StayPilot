using StayPilot.Application.DTOs.AiDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class AiTravelAssistantApiService : IAiTravelAssistantApiService
    {
        private readonly HttpClient _httpClient;

        public AiTravelAssistantApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<ResultAiTravelAnswerDto?> AskAsync(CreateAiTravelQuestionDto questionDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "api/travel/AiAssistant/ask",
                    questionDto);

                if (!response.IsSuccessStatusCode)
                {
                    return new ResultAiTravelAnswerDto
                    {
                        IsSuccessful = false,
                        ModelName = "StayPilot.Web",
                        ErrorMessage = "AI assistant request failed."
                    };
                }

                return await response.Content.ReadFromJsonAsync<ResultAiTravelAnswerDto>();
            }
            catch
            {
                return new ResultAiTravelAnswerDto
                {
                    IsSuccessful = false,
                    ModelName = "StayPilot.Web",
                    ErrorMessage = "AI assistant service could not be reached."
                };
            }
        }
    }
}