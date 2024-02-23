using System.Formats.Asn1;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ABMS_backend.Services
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return default(DateTime);

            if (reader.TokenType == JsonToken.Date)
                return (DateTime)reader.Value;

            if (reader.TokenType == JsonToken.Integer)
                return new DateTime(1970, 1, 1).AddMilliseconds((long)reader.Value);

            throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}");
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}



