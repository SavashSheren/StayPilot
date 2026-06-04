using StayPilot.Application.DTOs.CurrencyDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.Infrastructure.ExternalServices
{
    public class MockCurrencyProviderClient : ICurrencyProviderClient
    {
        public Task<ResultCurrencyRateDto> GetRateAsync(string baseCurrency, string targetCurrency)
        {
            var rate = GetMockRate(baseCurrency, targetCurrency);

            var result = new ResultCurrencyRateDto
            {
                BaseCurrency = baseCurrency,
                TargetCurrency = targetCurrency,
                Rate = rate,
                UpdatedDate = DateTime.UtcNow
            };

            return Task.FromResult(result);
        }

        private static decimal GetMockRate(string baseCurrency, string targetCurrency)
        {
            if (baseCurrency == targetCurrency)
            {
                return 1m;
            }

            return (baseCurrency, targetCurrency) switch
            {
                ("USD", "TRY") => 32.45m,
                ("EUR", "TRY") => 35.12m,
                ("GBP", "TRY") => 41.30m,
                ("USD", "EUR") => 0.92m,
                ("EUR", "USD") => 1.08m,
                ("GBP", "USD") => 1.27m,
                _ => 1.00m
            };
        }
    }
}