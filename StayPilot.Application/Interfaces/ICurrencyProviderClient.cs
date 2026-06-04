using StayPilot.Application.DTOs.CurrencyDtos;

namespace StayPilot.Application.Interfaces
{
    public interface ICurrencyProviderClient
    {
        Task<ResultCurrencyRateDto> GetRateAsync(string baseCurrency, string targetCurrency);
    }
}