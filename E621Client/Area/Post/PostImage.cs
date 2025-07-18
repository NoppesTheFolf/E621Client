using Newtonsoft.Json;
using Noppes.E621.Converters;
using Noppes.E621.Extensions;
using System;

namespace Noppes.E621
{
    public class PostImage
    {
        internal const string UrlProperty = "url";

        /// <summary>
        /// Width of the image.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Height of the image.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// The location of the image.
        /// </summary>
        [JsonProperty(UrlProperty), JsonConverter(typeof(UriConverter))]

        /// <summary>
        /// The image its file extension.
        /// </summary>
        [JsonIgnore]
        public string? FileExtension => Location.OriginalString.GetPathExtensionWithoutDot();
        public Uri? Location { get; set; } = null;
    }
}
