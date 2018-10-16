namespace Lob.Net.Models
{
    public class Error
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }

    public class ErrorResponse
    {
        public Error Error { get; set; }
    }
}
