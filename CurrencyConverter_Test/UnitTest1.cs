using CurrencyConverter.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace CurrencyConverter_Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetLatestRatesAsync_ReturnsRates_Test()
        {
            var service = new CurrencyService(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));
            var result = await service.GetLatestRatesAsync("EUR");
            Assert.NotNull(result);
            Assert.True(result.Rates.Count > 0);
        }

        [Fact]
        public async Task GetHistoricalRates_Test()
        {
            var service = new CurrencyService(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));
            DateTime start = DateTime.ParseExact("2020-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact("2020-01-31", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var result = await service.GetHistoricalRatesAsync(start,end,"USD",1,10);
            Assert.NotNull(result);
            Assert.True(result.TotalCount > 0);
        }

    }
}