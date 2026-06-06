namespace Cros.BusinessLogic.Dtos
{
    public class PaginationResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public Dictionary<string, object> Tags { get; set; } = [];
    }
}