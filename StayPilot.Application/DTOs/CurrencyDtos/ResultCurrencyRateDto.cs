namespace StayPilot.Application.DTOs.CurrencyDtos
{
    public class ResultCurrencyRateDto
    {
        public string BaseCurrency { get; set; } = string.Empty;

        public string TargetCurrency { get; set; } = string.Empty;

        public decimal Rate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}