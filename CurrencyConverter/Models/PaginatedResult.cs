namespace CurrencyConverter.Models
{
    public class PaginatedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public long TotalCount { get; set; }
        public T[] Items { get; set; } = Array.Empty<T>();
    }
}
