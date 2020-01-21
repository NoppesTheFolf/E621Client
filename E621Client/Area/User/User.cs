using Newtonsoft.Json;
using Noppes.E621.Converters;
using System;
using System.Collections.Generic;

namespace Noppes.E621
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        [JsonProperty("name")]
        public string Username { get; set; } = null!;

        /// <summary>
        /// ID of the user.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// The permission level of the user.
        /// </summary>
        [JsonProperty("level")]
        public UserPermissionLevel PermissionLevel { get; set; }

        /// <summary>
        /// When the user created their account.
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(UserDateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The ID of the post that the user uses as an avatar.
        /// </summary>
        [JsonProperty("avatar_id")]
        public int? AvatarId { get; set; }

        /// <summary>
        /// Statistics about the user.
        /// </summary>
        [JsonProperty("stats")]
        public UserStatistics Statistics { get; set; } = null!;

        /// <summary>
        /// Artist tags associated with the user.
        /// </summary>
        [JsonProperty("artist_tags")]
        public List<string> ArtistTags { get; set; }

        public User()
        {
            ArtistTags = new List<string>();
        }
    }
}
