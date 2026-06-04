using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.HotelDtos;
using StayPilot.Web.Services;
using StayPilot.Web.ViewModels;

namespace StayPilot.Web.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelSearchApiService _hotelSearchApiService;

        public HotelsController(IHotelSearchApiService hotelSearchApiService)
        {
            _hotelSearchApiService = hotelSearchApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(HotelSearchRequestDto searchRequest)
        {
            var model = new HotelSearchPageViewModel
            {
                SearchRequest = NormalizeSearchRequest(searchRequest)
            };

            if (string.IsNullOrWhiteSpace(model.SearchRequest.Destination))
            {
                model.SearchRequest.Destination = "Istanbul";
            }

            model.HasSearched = true;
            model.Hotels = await _hotelSearchApiService.SearchHotelsAsync(model.SearchRequest);

            if (!model.Hotels.Any())
            {
                model.ErrorMessage = "No hotels found for your search criteria. Try changing destination or price filters.";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction(nameof(Search));
            }

            var hotel = await _hotelSearchApiService.GetHotelDetailAsync(id);

            if (hotel is null)
            {
                TempData["HotelError"] = "Hotel detail could not be loaded.";

                return RedirectToAction(nameof(Search));
            }

            return View(hotel);
        }

        private static HotelSearchRequestDto NormalizeSearchRequest(HotelSearchRequestDto request)
        {
            if (request.CheckInDate == default)
            {
                request.CheckInDate = DateTime.Today.AddDays(7);
            }

            if (request.CheckOutDate == default || request.CheckOutDate <= request.CheckInDate)
            {
                request.CheckOutDate = request.CheckInDate.AddDays(4);
            }

            if (request.AdultCount <= 0)
            {
                request.AdultCount = 2;
            }

            if (request.RoomCount <= 0)
            {
                request.RoomCount = 1;
            }

            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            return request;
        }
    }
}