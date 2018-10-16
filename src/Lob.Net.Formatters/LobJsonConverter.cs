using Lob.Net.Models;
using Newtonsoft.Json;
using System;

namespace Lob.Net.Formatters
{
    public class LobJsonConverter : JsonConverter
    {
        private readonly LobSerializerSettings serializerSettings;

        public LobJsonConverter(
            LobSerializerSettings serializerSettings
        )
        {
            this.serializerSettings = serializerSettings;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.Assembly == typeof(LobSerializerSettings).Assembly;
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var serializer2 = JsonSerializer.Create(serializerSettings);
            var result = serializer2.Deserialize(reader, objectType);

            var lobEvent = result as LobEvent;
            if (lobEvent != null)
            {
                lobEvent.SerializerSettings = serializerSettings;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
