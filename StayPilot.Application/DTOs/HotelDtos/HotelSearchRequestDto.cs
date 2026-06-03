namespace StayPilot.Application.DTOs.HotelDtos
{
    public class HotelSearchRequestDto
    {
        public string Destination { get; set; } = string.Empty;

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int AdultCount { get; set; } = 2;

        public int RoomCount { get; set; } = 1;

        public int? ChildCount { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public int Page { get; set; } = 1;
    }
}