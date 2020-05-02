using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// An opinionated JSON converter that handles the conversion of the information associated with
    /// a post's images to and from a <see cref="PostImage"/> objects.
    /// </summary>
    internal class PostImageConverter<TPostImage> : JsonConverter<TPostImage?> where TPostImage : PostImage
    {
        public override void WriteJson(JsonWriter writer, TPostImage? value, JsonSerializer serializer) => throw new NotImplementedException();

        public override TPostImage? ReadJson(JsonReader reader, Type objectType, TPostImage? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            return token.Value<string?>(PostImage.UrlProperty) == null
                ? default
                : token.ToObject<TPostImage>();
        }
    }
}
