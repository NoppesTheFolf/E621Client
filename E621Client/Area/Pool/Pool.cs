using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;
using System.Collections.Generic;

namespace Noppes.E621
{
    /// <summary>
    /// Represents a pool. A pool is an ordered collection of posts that share a common theme.
    /// </summary>
    public class Pool
    {
        /// <summary>
        /// The ID of the pool.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name given to the pool.
        /// </summary>
        [JsonProperty("name"), JsonConverter(typeof(PoolNameJsonConverter))]
        public string Name { get; set; } = null!;

        /// <summary>
        /// When the pool was created.
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When the pool was last updated.
        /// </summary>
        [JsonProperty("updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// ID of the user who created the pool.
        /// </summary>
        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }

        /// <summary>
        /// Name of the user who created the pool.
        /// </summary>
        [JsonProperty("creator_name")]
        public string CreatorName { get; set; } = null!;

        /// <summary>
        /// Description of the pool. Null if there is no description.
        /// </summary>
        [JsonProperty("description"), JsonConverter(typeof(EmptyStringConverter))]
        public string? Description { get; set; }

        /// <summary>
        /// Whether or not the pool is still actively being updated with new posts.
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Category of the pool.
        /// </summary>
        [JsonProperty("category")]
        public PoolCategory Category { get; set; }

        /// <summary>
        /// Whether or not the pool has been deleted.
        /// </summary>
        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// IDs of the posts in the pool. Keep in mind that posts in a pool can be ordered and
        /// therefore this is an ordered collection.
        /// </summary>
        [JsonProperty("post_ids")]
        public HashSet<int> PostIds { get; set; } = null!;
    }
}
