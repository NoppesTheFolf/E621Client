using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;
using System.Collections.Generic;

namespace Noppes.E621
{
    /// <summary>
    /// Represents an artist on e621.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// ID of the artist.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the artist.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The last time the artist their information got updated.
        /// </summary>
        [JsonProperty("updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Whether or not the artist is still active. Unclear if this is in general or specifically
        /// on e621.
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Other names the artist might be known as.
        /// </summary>
        [JsonProperty("other_names")]
        public ICollection<string> OtherNames { get; set; } = null!;

        /// <summary>
        /// It is not known what this property means.
        /// </summary>
        [JsonProperty("group_name"), JsonConverter(typeof(EmptyStringConverter))]
        public string? GroupName { get; set; }

        /// <summary>
        /// Presumably the user ID of the artist if they happen to have an account on e621.
        /// </summary>
        [JsonProperty("linked_user_id")]
        public int? LinkedUserId { get; set; }

        /// <summary>
        /// When the artist entity got created.
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Whether or not the artist is banned.
        /// </summary>
        [JsonProperty("is_banned")]
        public bool IsBanned { get; set; }

        /// <summary>
        /// ID of the user that created this artist entity.
        /// </summary>
        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }

        /// <summary>
        /// If the information of this artist is locked. Aka if it can be edited.
        /// </summary>
        [JsonProperty("is_locked")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Notes about the artist.
        /// </summary>
        [JsonProperty("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// A collection of places (URLs) where the artist can be found. This includes platforms
        /// like Twitter and Fur Affinity.
        /// </summary>
        [JsonProperty("urls")]
        public ICollection<ArtistUrl> Urls { get; set; } = null!;
    }
}
