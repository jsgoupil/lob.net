using Lob.Net.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Lob.Net.Helpers
{
    internal class AddressConverter : JsonConverter<AddressRequest>
    {
        public override AddressRequest ReadJson(JsonReader reader, Type objectType, AddressRequest existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            switch (jObject.Type)
            {
                case JTokenType.String:
                    return new AddressRequest(jObject.Value<string>());
                case JTokenType.Object:
                    var obj = jObject.ToObject<Address>();
                    if (obj != null)
                    {
                        return new AddressRequest(obj);
                    }
                    break;
            }

            throw new Exception($"Can't read the object.");
        }

        public override void WriteJson(JsonWriter writer, AddressRequest value, JsonSerializer serializer)
        {
            if (!string.IsNullOrEmpty(value.AddressId))
            {
                serializer.Serialize(writer, value.AddressId);
            }
            else
            {
                serializer.Serialize(writer, value.AddressObject);
            }
        }
    }
}
