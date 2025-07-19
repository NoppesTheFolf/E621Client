using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// An opinionated JSON converter that handles the conversion of the information associated with
    /// a post's images to and from a <see cref="PostImage"/> objects.
    /// </summary>
    internal class PostImageConverter<TPostImage> : JsonConverter<TPostImage?> where TPostImage : PostImage
    {
        public override void WriteJson(JsonWriter writer, TPostImage? value, JsonSerializer serializer)
        {
            if (value == null) writer.WriteNull();
            else
            {
                JToken t = JToken.FromObject(value);

                JObject o = (JObject)t;
                o.WriteTo(writer);
            }
        }

        public override TPostImage? ReadJson(JsonReader reader, Type objectType, TPostImage? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            // Don't ignore image info on deleted posts, we want the width/height and hash
            return TokenToPostImage(token, existingValue);
        }

        public TPostImage TokenToPostImage(JToken token, TPostImage? existingValue)
        {
            return token.ToObject<TPostImage>()!;
        }
    }
}
