using Newtonsoft.Json;
using System.Collections.Generic;

namespace Noppes.E621
{
    /// <summary>
    /// Represents the relationships a post might have with other posts.
    /// </summary>
    public class Relationships
    {
        /// <summary>
        /// The post's parent. Null if the post doesn't have a parent post.
        /// </summary>
        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// Whether or not the post has any children.
        /// </summary>
        [JsonProperty("has_children")]
        public bool HasChildren { get; set; }

        /// <summary>
        /// Whether or not the post has any children which are active.
        /// </summary>
        [JsonProperty("has_active_children")]
        public bool HasActiveChildren { get; set; }

        /// <summary>
        /// The post IDs of the post's children.
        /// </summary>
        [JsonProperty("children")]
        public ICollection<int> Children { get; set; } = new List<int>();
    }
}
