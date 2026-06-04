using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Travel
{
    [Route("api/travel/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("rate")]
        public async Task<IActionResult> GetCurrencyRate(
            string baseCurrency = "USD",
            string targetCurrency = "TRY")
        {
            var value = await _currencyService.GetRateAsync(baseCurrency, targetCurrency);

            return Ok(value);
        }
    }
}