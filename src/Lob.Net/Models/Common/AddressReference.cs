using Lob.Net.Helpers;
using Newtonsoft.Json;

namespace Lob.Net.Models
{
    [JsonConverter(typeof(AddressReferenceConverter))]
    public class AddressReference
    {
        public string AddressId { get; }
        public AddressRequest AddressObject { get; }

        public AddressReference(string id)
        {
            AddressId = id;
        }

        public AddressReference(AddressRequest address)
        {
            AddressObject = address;
        }
    }
}
