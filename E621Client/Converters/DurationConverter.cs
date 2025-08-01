﻿using Newtonsoft.Json;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts duration (float) to/from TimeSpan.
    /// </summary>
    internal class DurationConverter : JsonConverter<TimeSpan?>
    {
        public override void WriteJson(JsonWriter writer, TimeSpan? value, JsonSerializer serializer)
        {
        if (value is null)
            writer.WriteNull();
        else // Write TimeSpan as total seconds (as a double)
            writer.WriteValue(value.Value.TotalSeconds);
        }

        public override TimeSpan? ReadJson(JsonReader reader, Type objectType, TimeSpan? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            if (reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer)
                return TimeSpan.FromSeconds(Convert.ToDouble(reader.Value));

            throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing TimeSpan.");
        }
    }
}
