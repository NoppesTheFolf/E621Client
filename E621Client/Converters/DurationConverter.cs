using Newtonsoft.Json;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts duration (float) to/from TimeSpan
    /// </summary>
    internal class DurationConverter : JsonConverter<TimeSpan?>
    {
        public override void WriteJson(JsonWriter writer, TimeSpan? value, JsonSerializer serializer)
        {
            if (value is null) writer.WriteNull();
            // Write TimeSpan as total seconds (as a double)
            else writer.WriteValue(value.Value.TotalSeconds);
        }

        public override TimeSpan? ReadJson(JsonReader reader, Type objectType, TimeSpan? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            if (reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer)
            {
                double totalSeconds = Convert.ToDouble(reader.Value);
                return TimeSpan.FromSeconds(totalSeconds);
            }

            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing TimeSpan.");
        }
    }
}
