using CurrencyConverter.Models;

namespace CurrencyConverter.Services
{
    public interface ICurrencyService
    {
        Task<ExchangeRateResponse> GetLatestRatesAsync(string baseCurrency);
        Task<PagedResult<ExchangeRateDto>> GetHistoricalRatesAsync(DateTime start, DateTime end, string baseCurrency, int page, int pageSize);
    }
}
