using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Noppes.E621.Converters
{
    internal class UserDateTimeOffsetConverter : StringJsonConverter<DateTimeOffset>
    {
        private const string Format = "yyyy-MM-dd HH:mm";

        protected override DateTimeOffset ReadString(string value, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            DateTime parsedDateTime = DateTime.ParseExact(value, Format, CultureInfo.InvariantCulture);

            return DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Utc);
        }

        public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
