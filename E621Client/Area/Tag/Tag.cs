using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;
using System.Collections.Generic;

namespace Noppes.E621
{
    /// <summary>
    /// Represents a tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// ID of the tag.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the tag.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The number of posts that make use of the tag.
        /// </summary>
        [JsonProperty("post_count")]
        public int Count { get; set; }

        /// <summary>
        /// Tags somehow related to this tag.
        /// </summary>
        [JsonProperty("related_tags"), JsonConverter(typeof(RelatedTagsConverter))]
        public ICollection<RelatedTag> RelatedTags { get; set; }

        /// <summary>
        /// The last time the related tags changed for this tag.
        /// </summary>
        [JsonProperty("related_tags_updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset RelatedTagsUpdatedAt { get; set; }

        /// <summary>
        /// The category the tag belongs in.
        /// </summary>
        [JsonProperty("category")]
        public TagCategory Category { get; set; }

        /// <summary>
        /// Whether or not the category of the tag is locked. The category of the tag can't be
        /// changed if this is the case.
        /// </summary>
        [JsonProperty("is_locked")]
        public bool IsCategoryLocked { get; set; }

        /// <summary>
        /// The point in time the tag was created.
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the tag for updated. Unsure which properties must be changed before this
        /// property changes too.
        /// </summary>
        [JsonProperty("updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        public Tag()
        {
            RelatedTags = new List<RelatedTag>();
        }
    }
}
