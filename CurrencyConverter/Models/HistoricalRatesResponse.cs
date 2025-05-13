namespace CurrencyConverter.Models
{
    public class HistoricalRatesResponse
    {
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; } = default!;
    }
}
