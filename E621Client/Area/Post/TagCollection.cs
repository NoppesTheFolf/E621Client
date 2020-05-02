using Newtonsoft.Json;
using System.Collections.Generic;

namespace Noppes.E621
{
    /// <summary>
    /// A collection of categorized tags.
    /// </summary>
    public class TagCollection
    {
        /// <summary>
        /// Tags that don't fall under any of the other categories.
        /// </summary>
        [JsonProperty("general")]
        public ICollection<string> General { get; set; } = new List<string>();

        /// <summary>
        /// Tags describing the species of a character or being shown in the post.
        /// </summary>
        [JsonProperty("species")]
        public ICollection<string> Species { get; set; } = new List<string>();

        /// <summary>
        /// Tags relating to the name(s) of a character(s) shown in the post.
        /// </summary>
        [JsonProperty("character")]
        public ICollection<string> Character { get; set; } = new List<string>();

        /// <summary>
        /// Tags relating to copyrighted elements or characters shown in the post.
        /// </summary>
        [JsonProperty("copyright")]
        public ICollection<string> Copyright { get; set; } = new List<string>();

        /// <summary>
        /// Tags that can be used to identify the artist(s) of a post.
        /// </summary>
        [JsonProperty("artist")]
        public ICollection<string> Artist { get; set; } = new List<string>();

        /// <summary>
        /// Tags that aren't valid.
        /// </summary>
        [JsonProperty("invalid")]
        public ICollection<string> Invalid { get; set; } = new List<string>();

        /// <summary>
        /// Tags that provide and/or correct specific outside information (not covered by copyright
        /// or character).
        /// </summary>
        [JsonProperty("lore")]
        public ICollection<string> Lore { get; set; } = new List<string>();

        /// <summary>
        /// Tags that relate to the technical side of the post's file or post itself.
        /// </summary>
        [JsonProperty("meta")]
        public ICollection<string> Meta { get; set; } = new List<string>();
    }
}
