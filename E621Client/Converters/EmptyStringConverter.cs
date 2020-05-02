using Newtonsoft.Json;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// An opinionated converter that treats a string without characters or only with whitespace
    /// characters as a null value.
    /// </summary>
    internal class EmptyStringConverter : StringJsonConverter<string?>
    {
        protected override string? ReadString(string value, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            string.IsNullOrWhiteSpace(value) ? null : value;

        protected override string AsString(string? value) =>
            value ?? string.Empty;
    }
}
