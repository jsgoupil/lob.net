using Lob.Net.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Lob.Net.Helpers
{
    internal class AddressReferenceConverter : JsonConverter<AddressReference>
    {
        public override AddressReference ReadJson(JsonReader reader, Type objectType, AddressReference existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            switch (jObject.Type)
            {
                case JTokenType.String:
                    return new AddressReference(jObject.Value<string>());
                case JTokenType.Object:
                    var obj = jObject.ToObject<AddressRequest>();
                    if (obj != null)
                    {
                        return new AddressReference(obj);
                    }
                    break;
            }

            throw new Exception($"Can't read the object.");
        }

        public override void WriteJson(JsonWriter writer, AddressReference value, JsonSerializer serializer)
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
