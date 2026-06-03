using StayPilot.Application.DTOs.ContactDtos;
using System.Net.Http.Json;

namespace StayPilot.Web.Services
{
    public class ContactMessageApiService : IContactMessageApiService
    {
        private readonly HttpClient _httpClient;

        public ContactMessageApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StayPilotApi");
        }

        public async Task<bool> SendContactMessageAsync(CreateContactMessageDto createContactMessageDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "api/public/ContactMessages",
                    createContactMessageDto);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}