namespace CurrencyConverter.Models
{
    public class ConvertResponse
    {
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public decimal Original { get; set; }
        public decimal Converted { get; set; }
    }
}
