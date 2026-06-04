using StayPilot.Application.DTOs.HotelDtos;

namespace StayPilot.Application.Interfaces
{
    public interface IHotelSearchService
    {
        Task<List<ResultHotelDto>> SearchHotelsAsync(HotelSearchRequestDto requestDto);

        Task<ResultHotelDetailDto?> GetHotelDetailAsync(string hotelId);
    }
}