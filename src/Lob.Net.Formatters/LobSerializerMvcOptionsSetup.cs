using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lob.Net.Formatters
{
    internal sealed class LobSerializerMvcOptionsSetup : IConfigureOptions<MvcJsonOptions>
    {
        private readonly LobSerializerSettings serializerSettings;

        public LobSerializerMvcOptionsSetup(
            LobSerializerSettings serializerSettings
        )
        {
            this.serializerSettings = serializerSettings;
        }

        public void Configure(MvcJsonOptions jsonOptions)
        {
            jsonOptions.SerializerSettings.Converters.Insert(0, new LobJsonConverter(serializerSettings));
        }
    }
}
