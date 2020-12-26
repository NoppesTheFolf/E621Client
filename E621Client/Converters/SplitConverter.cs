using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts a string into a collection by splitting it at the specified character.
    /// </summary>
    /// <typeparam name="T">The type the collection should be of.</typeparam>
    internal class SplitConverter<T> : StringJsonConverter<ICollection<T>>
    {
        public char Separator { get; }

        public SplitConverter(char separator)
        {
            Separator = separator;
        }

        protected override ICollection<T> ReadString(string value, Type objectType, ICollection<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<T>();

            return value
                .Split(Separator)
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(ConvertType)
                .ToList();
        }

        protected virtual T ConvertType(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        protected override string AsString(ICollection<T> value)
        {
            throw new NotImplementedException();
        }
    }
}
