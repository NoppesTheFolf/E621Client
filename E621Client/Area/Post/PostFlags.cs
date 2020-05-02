using Newtonsoft.Json;

namespace Noppes.E621
{
    public class PostFlags
    {
        /// <summary>
        /// If the post is pending approval.
        /// </summary>
        [JsonProperty("pending")]
        public bool IsPending { get; set; }

        /// <summary>
        /// If the post is flagged for deletion.
        /// </summary>
        [JsonProperty("flagged")]
        public bool IsFlagged { get; set; }

        /// <summary>
        /// If the post has it’s notes locked.
        /// </summary>
        [JsonProperty("note_locked")]
        public bool IsNoteLocked { get; set; }
        
        /// <summary>
        /// If the post’s status has been locked. Null if a post has been deleted.
        /// </summary>
        [JsonProperty("status_locked")]
        public bool? IsStatusLocked { get; set; }

        /// <summary>
        /// If the post’s rating has been locked.
        /// </summary>
        [JsonProperty("rating_locked")]
        public bool IsRatingLocked { get; set; }

        /// <summary>
        /// If the post has been deleted.
        /// </summary>
        [JsonProperty("deleted")]
        public bool IsDeleted { get; set; }
    }
}
