using CurrencyConverter.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Polly;
using System.Text.Json;

namespace CurrencyConverter.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

        public CurrencyService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }


        public async Task<PagedResult<ExchangeRateDto>> GetHistoricalRatesAsync(
        DateTime start, DateTime end, string baseCurrency, int page, int pageSize)
        {
            string url = $"https://api.frankfurter.app/{start:yyyy-MM-dd}..{end:yyyy-MM-dd}?base={baseCurrency}";
            var response = await _httpClient.GetFromJsonAsync<FrankfurterResponse>(url);

            if (response == null || response.Rates == null)
                return new PagedResult<ExchangeRateDto>();

            var pagedRates = response.Rates
                .OrderBy(k => k.Key) // Ensure chronological order
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(rate => new ExchangeRateDto { Date = rate.Key, Rates = rate.Value })
                .ToList();

            return new PagedResult<ExchangeRateDto>
            {
                Data = pagedRates,
                TotalCount = response.Rates.Count,
                Page = page,
                PageSize = pageSize
            };
        }


        public async Task<ExchangeRateResponse> GetLatestRatesAsync(string baseCurrency)
        {
            var cacheKey = $"rates_{baseCurrency}";
            if (_cache.TryGetValue(cacheKey, out ExchangeRateResponse cachedResponse))
                return cachedResponse;

            var response = await _retryPolicy.ExecuteAsync(() =>
                _httpClient.GetAsync($"https://api.frankfurter.app/latest?base={baseCurrency}"));

            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<ExchangeRateResponse>(await response.Content.ReadAsStringAsync());

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30)); // Cache for 30 minutes
            return result;
        }

        
    }
}
