using Newtonsoft.Json;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts a string representation of a <see cref="PostRating"/> into a <see cref="PostRating"/>.
    /// </summary>
    internal class PostRatingConverter : StringJsonConverter<PostRating>
    {
        protected override PostRating ReadString(string value, Type objectType, PostRating existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return PostRatingHelper.FromAbbreviation(value);
        }

        protected override string AsString(PostRating value)
        {
            return value.ToAbbreviation();
        }
    }
}
