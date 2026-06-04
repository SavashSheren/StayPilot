using StayPilot.Application.DTOs.HotelDtos;

namespace StayPilot.Web.ViewModels
{
    public class HotelSearchPageViewModel
    {
        public HotelSearchRequestDto SearchRequest { get; set; } = new();

        public List<ResultHotelDto> Hotels { get; set; } = new();

        public bool HasSearched { get; set; }

        public string? ErrorMessage { get; set; }
    }
}