using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts the annoying API response returned by an IQDB query into an <see cref="IqdbPost"/>.
    /// </summary>
    internal class IqdbPostConverter : JsonConverter<IqdbPost>
    {
        public override IqdbPost ReadJson(JsonReader reader, Type objectType, IqdbPost existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            var post = token["post"]["posts"].ToObject<IqdbApiPost>().AsPost();
            post.IqdbScore = token["score"].ToObject<float>();

            return post;
        }

        public override void WriteJson(JsonWriter writer, IqdbPost value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
