using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Lob.Net.Helpers
{
    internal class EnumConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : struct, Enum
    {
        public override TEnum ReadJson(JsonReader reader, Type objectType, TEnum existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    var snakeValue = new JValue(reader.Value).Value<string>();
                    var camelValue = snakeValue.ToPascalCase();

                    if (Enum.TryParse<TEnum>(camelValue, out var result))
                    {
                        return result;
                    }

                    throw new Exception($"Can't read the object {snakeValue}.");
            }


            throw new Exception($"Can't read the object.");
        }

        public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer)
        {
            var snakeValue = value.GetValue() ?? value.ToString().ToSnakeCase();
            serializer.Serialize(writer, snakeValue);
        }
    }
}
