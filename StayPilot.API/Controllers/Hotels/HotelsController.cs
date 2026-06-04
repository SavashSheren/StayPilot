using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.HotelDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Hotels
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelSearchService _hotelSearchService;

        public HotelsController(IHotelSearchService hotelSearchService)
        {
            _hotelSearchService = hotelSearchService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchHotels(HotelSearchRequestDto requestDto)
        {
            if (string.IsNullOrWhiteSpace(requestDto.Destination))
            {
                return BadRequest(new
                {
                    message = "Destination is required."
                });
            }

            if (requestDto.CheckInDate >= requestDto.CheckOutDate)
            {
                return BadRequest(new
                {
                    message = "Check-out date must be later than check-in date."
                });
            }

            var values = await _hotelSearchService.SearchHotelsAsync(requestDto);

            return Ok(values);
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelDetail(string hotelId)
        {
            var value = await _hotelSearchService.GetHotelDetailAsync(hotelId);

            if (value is null)
            {
                return NotFound(new
                {
                    message = "Hotel detail not found."
                });
            }

            return Ok(value);
        }
    }
}