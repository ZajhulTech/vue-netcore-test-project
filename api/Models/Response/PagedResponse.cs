namespace Api.Models.Response1
{
    public class PagedResponse<T>
    {
        public List<T> Raw { get; set; } = new();
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
