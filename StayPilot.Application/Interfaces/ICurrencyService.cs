using StayPilot.Application.DTOs.CurrencyDtos;

namespace StayPilot.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<ResultCurrencyRateDto> GetRateAsync(string baseCurrency, string targetCurrency);
    }
}