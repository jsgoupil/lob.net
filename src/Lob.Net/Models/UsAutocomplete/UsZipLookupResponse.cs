namespace Lob.Net.Models
{
    public class UsZipLookupResponse
    {
        public string Id { get; set; }
        public string ZipCode { get; set; }
        public ZipCodeType? ZipCodeType { get; set; }
        public UsCity[] Cities { get; set; }
        public string Object { get; set; }
    }
}
