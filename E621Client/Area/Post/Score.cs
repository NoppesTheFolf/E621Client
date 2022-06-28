using Newtonsoft.Json;

namespace Noppes.E621
{
    public class Score
    {
        /// <summary>
        /// The number of upvotes the post has.
        /// </summary>
        [JsonProperty("up")]
        public int Up { get; set; }

        /// <summary>
        /// The number of downvotes the post has.
        /// </summary>
        [JsonProperty("down")]
        public int Down { get; set; }

        /// <summary>
        /// The total score the post has. Theoretically, this is the number of upvotes subtracted by
        /// the number of downvotes. However, what the API returns differs from this.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
