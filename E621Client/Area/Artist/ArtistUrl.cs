using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;

namespace Noppes.E621
{
    /// <summary>
    /// Represents a URL at which an artist can be found.
    /// </summary>
    public class ArtistUrl
    {
        /// <summary>
        /// ID of this artist URL.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the artist associated with this URL.
        /// </summary>
        [JsonProperty("artist_id")]
        public int ArtistId { get; set; }

        /// <summary>
        /// The actual URL itself at which the artist can be found.
        /// </summary>
        [JsonProperty("url"), JsonConverter(typeof(UriConverter))]
        public Uri Location { get; set; } = null!;

        /// <summary>
        /// When this URL get added to the artist's URL collection.
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When this URL entity its associated information got updated the last time.
        /// </summary>
        [JsonProperty("updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Whether or not this URL is still active/valid. Might be a dead URL if this is false.
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}
