namespace Lob.Net.Models
{
    public class ListResponse<T>
    {
        public T[] Data { get; set; }
        public int? Count { get; set; }
        public string Object { get; set; }
    }
}
