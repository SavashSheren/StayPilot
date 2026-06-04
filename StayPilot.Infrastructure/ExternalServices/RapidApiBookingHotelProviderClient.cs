using Microsoft.Extensions.Configuration;
using StayPilot.Application.DTOs.HotelDtos;
using StayPilot.Application.Interfaces;
using System.Globalization;
using System.Text.Json;

namespace StayPilot.Infrastructure.ExternalServices
{
    public class RapidApiBookingHotelProviderClient : IHotelProviderClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RapidApiBookingHotelProviderClient(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<ResultHotelDto>> SearchHotelsAsync(HotelSearchRequestDto requestDto)
        {
            var rapidApiKey = _configuration["HotelProvider:RapidApiKey"];
            var rapidApiHost = _configuration["HotelProvider:RapidApiHost"];

            if (string.IsNullOrWhiteSpace(rapidApiKey) || string.IsNullOrWhiteSpace(rapidApiHost))
            {
                return new List<ResultHotelDto>();
            }

            var destination = await GetDestinationAsync(
                requestDto.Destination,
                rapidApiKey,
                rapidApiHost);

            if (destination is null)
            {
                return new List<ResultHotelDto>();
            }

            using var hotelsJson = await SearchHotelsFromRapidApiAsync(
                destination.Value.DestinationId,
                destination.Value.SearchType,
                requestDto,
                rapidApiKey,
                rapidApiHost);

            if (hotelsJson is null)
            {
                return new List<ResultHotelDto>();
            }

            return MapHotels(hotelsJson.RootElement, requestDto);
        }

        public Task<ResultHotelDetailDto?> GetHotelDetailAsync(string hotelId)
        {
            if (string.IsNullOrWhiteSpace(hotelId))
            {
                return Task.FromResult<ResultHotelDetailDto?>(null);
            }

            var detail = new ResultHotelDetailDto
            {
                HotelId = hotelId,
                HotelName = $"Booking Hotel #{hotelId}",
                Description = "This hotel detail is generated from the RapidAPI Booking provider foundation. In the next stage, the dedicated hotel detail endpoint will be connected.",
                CityName = "Istanbul",
                CountryName = "Turkey",
                Address = "Istanbul, Turkey",
                Latitude = 41.0082m,
                Longitude = 28.9784m,
                Price = 0,
                Currency = "USD",
                ReviewScore = 0,
                ReviewScoreWord = "Provider Detail Pending",
                BookingUrl = "https://www.booking.com",
                ImageUrls = new List<string>
                {
                    "https://images.unsplash.com/photo-1566073771259-6a8506099945",
                    "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa",
                    "https://images.unsplash.com/photo-1582719508461-905c673771fd"
                },
                Facilities = new List<string>
                {
                    "Real hotel list connected",
                    "Detail endpoint will be connected next",
                    "RapidAPI Booking provider active"
                }
            };

            return Task.FromResult<ResultHotelDetailDto?>(detail);
        }

        private async Task<(string DestinationId, string SearchType)?> GetDestinationAsync(
            string destination,
            string rapidApiKey,
            string rapidApiHost)
        {
            var safeDestination = string.IsNullOrWhiteSpace(destination)
                ? "Istanbul"
                : destination.Trim();

            var url = $"https://{rapidApiHost}/api/v1/hotels/searchDestination?query={Uri.EscapeDataString(safeDestination)}";

            using var request = CreateRapidApiRequest(url, rapidApiKey, rapidApiHost);
            using var response = await _httpClient.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"RapidAPI SearchDestination failed. StatusCode: {(int)response.StatusCode}. Body: {responseContent}");
            }

            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return null;
            }

            using var document = JsonDocument.Parse(responseContent);

            if (!TryGetProperty(document.RootElement, "data", out var dataElement) ||
                dataElement.ValueKind != JsonValueKind.Array ||
                dataElement.GetArrayLength() == 0)
            {
                return null;
            }

            var firstDestination = dataElement[0];

            var destinationId = GetString(firstDestination, "dest_id");
            var searchType = GetString(firstDestination, "search_type");

            if (string.IsNullOrWhiteSpace(destinationId) || string.IsNullOrWhiteSpace(searchType))
            {
                return null;
            }

            return (destinationId, searchType);
        }

        private async Task<JsonDocument?> SearchHotelsFromRapidApiAsync(
            string destinationId,
            string searchType,
            HotelSearchRequestDto requestDto,
            string rapidApiKey,
            string rapidApiHost)
        {
            var arrivalDate = requestDto.CheckInDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var departureDate = requestDto.CheckOutDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            var queryParameters = new List<string>
            {
                $"dest_id={Uri.EscapeDataString(destinationId)}",
                $"search_type={Uri.EscapeDataString(searchType.ToLowerInvariant())}",
                $"arrival_date={arrivalDate}",
                $"departure_date={departureDate}",
                $"adults={Math.Max(1, requestDto.AdultCount)}",
                $"room_qty={Math.Max(1, requestDto.RoomCount)}",
                $"page_number={Math.Max(1, requestDto.Page)}",
                "units=metric",
                "temperature_unit=c",
                "languagecode=en-us",
                "currency_code=USD",
                "location=US"
            };

            if (requestDto.MinPrice.HasValue && requestDto.MinPrice.Value > 0)
            {
                queryParameters.Add($"price_min={requestDto.MinPrice.Value.ToString(CultureInfo.InvariantCulture)}");
            }

            if (requestDto.MaxPrice.HasValue && requestDto.MaxPrice.Value > 0)
            {
                queryParameters.Add($"price_max={requestDto.MaxPrice.Value.ToString(CultureInfo.InvariantCulture)}");
            }

            var url = $"https://{rapidApiHost}/api/v1/hotels/searchHotels?{string.Join("&", queryParameters)}";

            using var request = CreateRapidApiRequest(url, rapidApiKey, rapidApiHost);
            using var response = await _httpClient.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"RapidAPI SearchHotels failed. StatusCode: {(int)response.StatusCode}. Body: {responseContent}");
            }

            if (string.IsNullOrWhiteSpace(responseContent))
            {
                return null;
            }

            return JsonDocument.Parse(responseContent);
        }

        private static HttpRequestMessage CreateRapidApiRequest(
            string url,
            string rapidApiKey,
            string rapidApiHost)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("x-rapidapi-key", rapidApiKey);
            request.Headers.Add("x-rapidapi-host", rapidApiHost);
            request.Headers.Add("Accept", "application/json");

            return request;
        }

        private static List<ResultHotelDto> MapHotels(JsonElement rootElement, HotelSearchRequestDto requestDto)
        {
            var hotelsArray = GetHotelsArray(rootElement);

            if (hotelsArray is null)
            {
                return new List<ResultHotelDto>();
            }

            var hotels = new List<ResultHotelDto>();

            foreach (var hotelElement in hotelsArray.Value.EnumerateArray())
            {
                var hotel = MapHotel(hotelElement, requestDto);

                if (!string.IsNullOrWhiteSpace(hotel.HotelId) &&
                    !string.IsNullOrWhiteSpace(hotel.HotelName))
                {
                    hotels.Add(hotel);
                }
            }

            return hotels;
        }

        private static ResultHotelDto MapHotel(JsonElement hotelElement, HotelSearchRequestDto requestDto)
        {
            var hotelId =
                GetStringByPath(hotelElement, "hotel_id") ??
                GetStringByPath(hotelElement, "hotelId") ??
                GetStringByPath(hotelElement, "id") ??
                GetStringByPath(hotelElement, "property.id") ??
                GetStringByPath(hotelElement, "property.hotel_id") ??
                Guid.NewGuid().ToString();

            var hotelName =
                GetStringByPath(hotelElement, "hotel_name") ??
                GetStringByPath(hotelElement, "hotelName") ??
                GetStringByPath(hotelElement, "name") ??
                GetStringByPath(hotelElement, "property.name") ??
                GetStringByPath(hotelElement, "property.hotelName") ??
                "Booking Hotel";

            var imageUrl =
                GetFirstStringFromArrayByPath(hotelElement, "property.photoUrls") ??
                GetFirstStringFromArrayByPath(hotelElement, "photoUrls") ??
                GetFirstStringFromArrayByPath(hotelElement, "photos") ??
                GetStringByPath(hotelElement, "main_photo_url") ??
                GetStringByPath(hotelElement, "photoMainUrl") ??
                GetStringByPath(hotelElement, "property.photoMainUrl") ??
                GetStringByPath(hotelElement, "property.mainPhotoUrl") ??
                "https://images.unsplash.com/photo-1566073771259-6a8506099945";

            var price =
                GetDecimalByPath(hotelElement, "property.priceBreakdown.grossPrice.value") ??
                GetDecimalByPath(hotelElement, "priceBreakdown.grossPrice.value") ??
                GetDecimalByPath(hotelElement, "price.value") ??
                GetDecimalByPath(hotelElement, "grossPrice.value") ??
                0m;

            var currency =
                GetStringByPath(hotelElement, "property.priceBreakdown.grossPrice.currency") ??
                GetStringByPath(hotelElement, "priceBreakdown.grossPrice.currency") ??
                GetStringByPath(hotelElement, "price.currency") ??
                GetStringByPath(hotelElement, "grossPrice.currency") ??
                "USD";

            var reviewScore =
                GetDoubleByPath(hotelElement, "property.reviewScore") ??
                GetDoubleByPath(hotelElement, "reviewScore") ??
                GetDoubleByPath(hotelElement, "review_score") ??
                0d;

            var reviewScoreWord =
                GetStringByPath(hotelElement, "property.reviewScoreWord") ??
                GetStringByPath(hotelElement, "reviewScoreWord") ??
                GetStringByPath(hotelElement, "review_score_word") ??
                "Guest rating";

            var address =
                GetStringByPath(hotelElement, "property.wishlistName") ??
                GetStringByPath(hotelElement, "property.address") ??
                GetStringByPath(hotelElement, "wishlistName") ??
                GetStringByPath(hotelElement, "address") ??
                requestDto.Destination;

            var country =
                GetStringByPath(hotelElement, "property.countryCode") ??
                GetStringByPath(hotelElement, "property.country") ??
                GetStringByPath(hotelElement, "country") ??
                "Turkey";

            return new ResultHotelDto
            {
                HotelId = hotelId,
                HotelName = hotelName,
                CityName = requestDto.Destination,
                CountryName = country,
                Address = address,
                ImageUrl = imageUrl,
                Price = Math.Round(price, 2),
                Currency = currency,
                ReviewScore = reviewScore,
                ReviewScoreWord = reviewScoreWord,
                BookingUrl = "https://www.booking.com"
            };
        }

        private static JsonElement? GetHotelsArray(JsonElement rootElement)
        {
            if (TryGetProperty(rootElement, "data", out var dataElement))
            {
                if (dataElement.ValueKind == JsonValueKind.Object)
                {
                    if (TryGetProperty(dataElement, "hotels", out var hotelsElement) &&
                        hotelsElement.ValueKind == JsonValueKind.Array)
                    {
                        return hotelsElement;
                    }

                    if (TryGetProperty(dataElement, "result", out var resultElement) &&
                        resultElement.ValueKind == JsonValueKind.Array)
                    {
                        return resultElement;
                    }

                    if (TryGetProperty(dataElement, "results", out var resultsElement) &&
                        resultsElement.ValueKind == JsonValueKind.Array)
                    {
                        return resultsElement;
                    }

                    if (TryGetProperty(dataElement, "searchResults", out var searchResultsElement) &&
                        searchResultsElement.ValueKind == JsonValueKind.Array)
                    {
                        return searchResultsElement;
                    }
                }

                if (dataElement.ValueKind == JsonValueKind.Array)
                {
                    return dataElement;
                }
            }

            if (TryGetProperty(rootElement, "hotels", out var rootHotelsElement) &&
                rootHotelsElement.ValueKind == JsonValueKind.Array)
            {
                return rootHotelsElement;
            }

            if (TryGetProperty(rootElement, "result", out var rootResultElement) &&
                rootResultElement.ValueKind == JsonValueKind.Array)
            {
                return rootResultElement;
            }

            if (TryGetProperty(rootElement, "results", out var rootResultsElement) &&
                rootResultsElement.ValueKind == JsonValueKind.Array)
            {
                return rootResultsElement;
            }

            return null;
        }

        private static string? GetString(JsonElement element, string propertyName)
        {
            if (!TryGetProperty(element, propertyName, out var value))
            {
                return null;
            }

            return value.ValueKind switch
            {
                JsonValueKind.String => value.GetString(),
                JsonValueKind.Number => value.ToString(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                _ => null
            };
        }

        private static string? GetStringByPath(JsonElement element, string path)
        {
            if (!TryGetElementByPath(element, path, out var value))
            {
                return null;
            }

            return value.ValueKind switch
            {
                JsonValueKind.String => value.GetString(),
                JsonValueKind.Number => value.ToString(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                _ => null
            };
        }

        private static decimal? GetDecimalByPath(JsonElement element, string path)
        {
            if (!TryGetElementByPath(element, path, out var value))
            {
                return null;
            }

            if (value.ValueKind == JsonValueKind.Number && value.TryGetDecimal(out var decimalValue))
            {
                return decimalValue;
            }

            if (value.ValueKind == JsonValueKind.String &&
                decimal.TryParse(value.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValue))
            {
                return parsedValue;
            }

            return null;
        }

        private static double? GetDoubleByPath(JsonElement element, string path)
        {
            if (!TryGetElementByPath(element, path, out var value))
            {
                return null;
            }

            if (value.ValueKind == JsonValueKind.Number && value.TryGetDouble(out var doubleValue))
            {
                return doubleValue;
            }

            if (value.ValueKind == JsonValueKind.String &&
                double.TryParse(value.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValue))
            {
                return parsedValue;
            }

            return null;
        }

        private static string? GetFirstStringFromArrayByPath(JsonElement element, string path)
        {
            if (!TryGetElementByPath(element, path, out var value) ||
                value.ValueKind != JsonValueKind.Array ||
                value.GetArrayLength() == 0)
            {
                return null;
            }

            var firstValue = value[0];

            return firstValue.ValueKind == JsonValueKind.String
                ? firstValue.GetString()
                : null;
        }

        private static bool TryGetElementByPath(JsonElement element, string path, out JsonElement value)
        {
            value = element;

            var parts = path.Split('.', StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (!TryGetProperty(value, part, out value))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetProperty(JsonElement element, string propertyName, out JsonElement value)
        {
            if (element.ValueKind == JsonValueKind.Object &&
                element.TryGetProperty(propertyName, out value))
            {
                return true;
            }

            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in element.EnumerateObject())
                {
                    if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
                    {
                        value = property.Value;
                        return true;
                    }
                }
            }

            value = default;
            return false;
        }
    }
}