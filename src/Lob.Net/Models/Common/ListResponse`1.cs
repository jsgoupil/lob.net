namespace Lob.Net.Models
{
    public class ListResponse<T>
    {
        public T[] Data { get; set; }
        public int Count { get; set; }
        public string Object { get; set; }
        public string PreviousUrl { get; set; }
        public string NextUrl { get; set; }
        public int? TotalCount { get; set; }
    }
}
