namespace CurrencyConverter.Models
{
    public class ConvertRequest
    {
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public decimal Amount { get; set; }
        public string BaseCurrency {  get; set; } = default!;
    }
}
