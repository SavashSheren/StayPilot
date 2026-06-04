using StayPilot.Application.DTOs.CurrencyDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyProviderClient _currencyProviderClient;

        public CurrencyService(ICurrencyProviderClient currencyProviderClient)
        {
            _currencyProviderClient = currencyProviderClient;
        }

        public async Task<ResultCurrencyRateDto> GetRateAsync(string baseCurrency, string targetCurrency)
        {
            if (string.IsNullOrWhiteSpace(baseCurrency))
            {
                baseCurrency = "USD";
            }

            if (string.IsNullOrWhiteSpace(targetCurrency))
            {
                targetCurrency = "TRY";
            }

            return await _currencyProviderClient.GetRateAsync(
                baseCurrency.ToUpperInvariant(),
                targetCurrency.ToUpperInvariant());
        }
    }
}