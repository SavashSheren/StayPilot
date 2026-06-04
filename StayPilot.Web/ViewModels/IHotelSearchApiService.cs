using StayPilot.Application.DTOs.HotelDtos;

namespace StayPilot.Web.Services
{
    public interface IHotelSearchApiService
    {
        Task<List<ResultHotelDto>> SearchHotelsAsync(HotelSearchRequestDto requestDto);

        Task<ResultHotelDetailDto?> GetHotelDetailAsync(string hotelId);
    }
}