using Newtonsoft.Json;

namespace Noppes.E621
{
    public class PostSampleImage : PostImage
    {
        /// <summary>
        /// Whether the sample image is the exact same image as the source image.
        /// </summary>
        [JsonProperty("has", NullValueHandling = NullValueHandling.Ignore)]
        public bool SameAsSource { get; set; } = true;

        /* A null value is present in case e621 is unable to generate a sample.
         This is the case in Adobe Flash (SWF) files, for example. This means the given sample URL
         is always the same as the source URL. */
    }
}
