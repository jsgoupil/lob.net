using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lob.Net
{
    public class LobSerializerSettings : JsonSerializerSettings
    {
        public LobSerializerSettings()
            : base()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
