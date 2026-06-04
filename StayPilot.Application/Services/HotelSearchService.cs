using StayPilot.Application.DTOs.HotelDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.Application.Services
{
    public class HotelSearchService : IHotelSearchService
    {
        private readonly IHotelProviderClient _hotelProviderClient;

        public HotelSearchService(IHotelProviderClient hotelProviderClient)
        {
            _hotelProviderClient = hotelProviderClient;
        }

        public async Task<List<ResultHotelDto>> SearchHotelsAsync(HotelSearchRequestDto requestDto)
        {
            if (requestDto.CheckInDate >= requestDto.CheckOutDate)
            {
                return new List<ResultHotelDto>();
            }

            if (requestDto.AdultCount <= 0)
            {
                requestDto.AdultCount = 2;
            }

            if (requestDto.RoomCount <= 0)
            {
                requestDto.RoomCount = 1;
            }

            if (requestDto.Page <= 0)
            {
                requestDto.Page = 1;
            }

            return await _hotelProviderClient.SearchHotelsAsync(requestDto);
        }

        public async Task<ResultHotelDetailDto?> GetHotelDetailAsync(string hotelId)
        {
            if (string.IsNullOrWhiteSpace(hotelId))
            {
                return null;
            }

            return await _hotelProviderClient.GetHotelDetailAsync(hotelId);
        }
    }
}