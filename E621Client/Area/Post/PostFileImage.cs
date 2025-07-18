using Newtonsoft.Json;
using Noppes.E621.Converters;
using Noppes.E621.Extensions;

namespace Noppes.E621
{
    public class PostFileImage : PostImage
    {
        /// <summary>
        /// The size of the image in bytes.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// The image its MD5 hash.
        /// </summary>
        [JsonProperty("md5")]
        public string Md5 { get; set; } = null!;

        /// <summary>
        /// The file extension of the image as according to the website or database
        /// </summary>
        [JsonProperty("ext")]
        public string? DatabaseExtension { get; set; } = null;

        /// <summary>
        /// The image's file extension.
        /// </summary>
        [JsonIgnore]
        public override string? FileExtension => Location == null ? DatabaseExtension : Location.OriginalString.GetPathExtensionWithoutDot();
    }
}
