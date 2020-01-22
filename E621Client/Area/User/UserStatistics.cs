using Newtonsoft.Json;

namespace Noppes.E621
{
    /// <summary>
    /// Contains statistics about a user.
    /// </summary>
    public class UserStatistics
    {
        /// <summary>
        /// The total number of active posts maintained by the user.
        /// </summary>
        [JsonProperty("post_count")]
        public int ActivePostsCount { get; set; }

        /// <summary>
        /// The total number of posts deleted which the user maintained.
        /// </summary>
        [JsonProperty("del_post_count")]
        public int DeletedPostsCount { get; set; }

        /// <summary>
        /// The total number of edits the user has made to the tags of posts.
        /// </summary>
        [JsonProperty("edit_count")]
        public int TagEditsCount { get; set; }

        /// <summary>
        /// The total number of favorite posts the user has.
        /// </summary>
        [JsonProperty("favorite_count")]
        public int FavoritePostsCount { get; set; }

        /// <summary>
        /// The total number of edits the user has made on the wiki.
        /// </summary>
        [JsonProperty("wiki_count")]
        public int WikiEditsCount { get; set; }

        /// <summary>
        /// The total number of forum posts created by the user.
        /// </summary>
        [JsonProperty("forum_post_count")]
        public int ForumPostsCreatedCount { get; set; }

        /// <summary>
        /// The total number of note edits by the user.
        /// </summary>
        [JsonProperty("note_count")]
        public int NoteEditsCount { get; set; }

        /// <summary>
        /// The total number of comments placed by the user.
        /// </summary>
        [JsonProperty("comment_count")]
        public int CommentsPlacedCount { get; set; }

        /// <summary>
        /// The total number of blips posted by the user.
        /// </summary>
        [JsonProperty("blip_count")]
        public int BlipsPostedCount { get; set; }

        /// <summary>
        /// The total number of sets maintained by the user.
        /// </summary>
        [JsonProperty("set_count")]
        public int SetsMaintainedCount { get; set; }

        /// <summary>
        /// The total number of changes made to pools.
        /// </summary>
        [JsonProperty("pool_update_count")]
        public int PoolChangesCount { get; set; }

        /// <summary>
        /// The total number of positive records the user has received.
        /// </summary>
        [JsonProperty("pos_user_records")]
        public int PositiveUserRecords { get; set; }

        /// <summary>
        /// The total number of neutral records the user has received.
        /// </summary>
        [JsonProperty("neutral_user_records")]
        public int NeutralUserRecords { get; set; }

        /// <summary>
        /// The total number of negative records the user has received.
        /// </summary>
        [JsonProperty("neg_user_records")]
        public int NegativeUserRecords { get; set; }
    }
}
