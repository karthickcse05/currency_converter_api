namespace CurrencyConverter.Models
{
    public class ExchangeRateResponse
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
        public DateTime Date { get; set; }
    }

    public class ExchangeRateDto
    {
        public string Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }

    public class FrankfurterResponse
    {
        public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; }
    }

    public class PagedResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
