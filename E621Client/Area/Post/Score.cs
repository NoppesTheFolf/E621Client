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
        /// The total score the post has. This is the number of upvotes subtracted by the number of downvotes.
        /// </summary>
        [JsonIgnore]
        public int Total => Up - Down;
    }
}
