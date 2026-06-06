namespace Cros.DataAccess.Models
{
    public class PaginationResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public Dictionary<string, object> Tags { get; set; } = [];
    }
}