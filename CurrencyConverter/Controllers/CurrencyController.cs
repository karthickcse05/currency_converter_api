using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("rates/{baseCurrency}")]
        public async Task<IActionResult> GetLatestRates(string baseCurrency)
        {
            var result = await _currencyService.GetLatestRatesAsync(baseCurrency);
            return Ok(result);
        }

        [HttpPost("convert")]
        public async Task<IActionResult> ConvertCurrency([FromBody] ConvertRequest request)
        {
            var excludedCurrencies = new List<string> { "TRY", "PLN", "THB", "MXN" };
            if (excludedCurrencies.Contains(request.From) || excludedCurrencies.Contains(request.To))
                return BadRequest("Unsupported currency.");

            var rates = await _currencyService.GetLatestRatesAsync(request.BaseCurrency);
            if (!rates.Rates.ContainsKey(request.To.ToUpper()))
                return BadRequest("Currency not available.");

            var convertedAmount = request.Amount * rates.Rates[request.To.ToUpper()];
            return Ok(new { ConvertedAmount = convertedAmount });
        }

        [HttpGet("historical")]
        public async Task<IActionResult> GetHistoricalRates(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end,
        [FromQuery] string baseCurrency = "USD",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _currencyService.GetHistoricalRatesAsync(start, end, baseCurrency, page, pageSize);
            return Ok(result);
        }
    }
}
