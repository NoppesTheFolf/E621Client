using Newtonsoft.Json;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Provides a way of serializing objects into and deserializing objects
    /// from a single JSON value as a string.
    /// </summary>
    /// <typeparam name="T">Type to convert to and from.</typeparam>
    internal abstract class StringJsonConverter<T> : JsonConverter<T>
    {
        public sealed override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (!(reader.Value is string value))
                throw new InvalidOperationException($"Value from {reader.GetType().Name} must be a string.");

            return ReadString(value, objectType, existingValue, hasExistingValue, serializer);
        }

        protected abstract T ReadString(string value, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer);

        public sealed override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            string valueAsString = AsString(value);

            serializer.Serialize(writer, valueAsString);
        }

        protected abstract string AsString(T value);
    }
}
