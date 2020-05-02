using Newtonsoft.Json;

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
    }
}
