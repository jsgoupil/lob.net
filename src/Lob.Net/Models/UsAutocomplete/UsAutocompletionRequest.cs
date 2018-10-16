namespace Lob.Net.Models
{
    public class UsAutocompletionRequest
    {
        public string AddressPrefix { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool GeoIpSort { get; set; }
    }
}
