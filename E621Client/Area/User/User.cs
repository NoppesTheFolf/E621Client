using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Noppes.E621
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// When the user registered.
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The name of the user. Generally referred to as the username.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The 'level' a user has on e621. This is more commonly known as a role.
        /// </summary>
        [JsonProperty("level")]
        public UserPermissionLevel Level { get; set; }

        /// <summary>
        /// The number of posts the user has uploaded/created.
        /// </summary>
        [JsonProperty("post_upload_count")]
        public int PostUploadCount { get; set; }

        /// <summary>
        /// The number of times the user updated a post.
        /// </summary>
        [JsonProperty("post_update_count")]
        public int PostUpdateCount { get; set; }

        /// <summary>
        /// Whether or not the user is banned.
        /// </summary>
        [JsonProperty("is_banned")]
        public bool IsBanned { get; set; }

        /// <summary>
        /// Whether or not the user can approve posts.
        /// </summary>
        [JsonProperty("can_approve_posts")]
        public bool CanApprovePosts { get; set; }

        /// <summary>
        /// Whether or not the user can upload without limits.
        /// </summary>
        [JsonProperty("can_upload_free")]
        public bool CanUploadFree { get; set; }

        /// <summary>
        /// The ID of the post that the user uses as an avatar.
        /// </summary>
        [JsonProperty("avatar_id")]
        public int? AvatarId { get; set; }
    }
}
