using Newtonsoft.Json;
using Noppes.E621.Converters;

namespace Noppes.E621
{
    /// <summary>
    /// A <see cref="Post"/> returned based on an IQDB query. Note that the sample width, sample
    /// height, preview width and preview height are not available for this <see cref="Post"/>
    /// instance due to e621's API not returning them.
    /// </summary>
    [JsonConverter(typeof(IqdbPostConverter))]
    public class IqdbPost : Post
    {
        /// <summary>
        /// A score that tells something about how similar the post's image is to the submitted image.
        /// </summary>
        public float IqdbScore { get; set; }
    }
}
