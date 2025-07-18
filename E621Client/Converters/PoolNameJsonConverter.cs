using Newtonsoft.Json;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Pool names have an underscore in them (so the name "My Pool" in the received JSON is
    /// "My_Pool") because ???. Doesn't make much sense conceptually and is inconvenient for users
    /// of this library. This converter removes those underscores.
    /// </summary>
    internal class PoolNameJsonConverter : StringJsonConverter<string>
    {
        protected override string ReadString(string value, Type objectType, string? existingValue, bool hasExistingValue, JsonSerializer serializer) =>
            value.Replace('_', ' ');

        protected override string AsString(string? value) =>
            value?.Replace(' ', '_') ?? String.Empty;
    }
}
