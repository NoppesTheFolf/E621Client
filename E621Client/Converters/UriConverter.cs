using Newtonsoft.Json;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts a string representation of a URI into an instance of <see cref="Uri"/>.
    /// </summary>
    internal class UriConverter : StringJsonConverter<Uri?>
    {
        protected override Uri? ReadString(string value, Type objectType, Uri? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return string.IsNullOrWhiteSpace(value) ? null : new Uri(value, UriKind.RelativeOrAbsolute);
        }

        protected override string AsString(Uri? value)
        {
            return value?.OriginalString ?? String.Empty;
        }
    }
}
