using Lob.Net.Helpers;
using Newtonsoft.Json;

namespace Lob.Net.Models
{
    [JsonConverter(typeof(AddressConverter))]
    public class AddressRequest
    {
        public string AddressId { get; }
        public Address AddressObject { get; }

        public AddressRequest(string id)
        {
            AddressId = id;
        }

        public AddressRequest(Address address)
        {
            AddressObject = address;
        }
    }
}
