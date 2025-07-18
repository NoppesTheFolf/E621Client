using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noppes.E621
{
    public class Flag
    {
        /// <summary>
        /// The ID of the flag
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// When the flag was created.
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the flag was updated. Null in case the flag has never been updated before.
        /// </summary>
        [JsonProperty("updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// The ID of the affected post
        /// </summary>
        [JsonProperty("post_id")]
        public int PostId { get; set; }

        /// <summary>
        /// The ID of the user who submitted the flag. Null if the creator has hid their ID.
        /// </summary>
        [JsonProperty("creator_id")]
        public int? CreatorId { get; set; }

        /// <summary>
        /// The flag's description, containing the reason for the flag.
        /// </summary>
        [JsonProperty("reason"), JsonConverter(typeof(EmptyStringConverter))]
        public string? Reason { get; set; }

        /// <summary>
        /// True if the flag/deletion has been reverted.
        /// </summary>
        [JsonProperty("is_resolved")]
        public bool IsResolved { get; set; }

        /// <summary>
        /// Whether the post has been deleted.
        /// </summary>
        [JsonProperty("is_deletion")]
        public bool IsDeletion { get; set; }

        /// <summary>
        /// The type of the flag, currently undocumented, known values are "flag" and "deletion"
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(EmptyStringConverter))]
        public string? Type { get; set; }
    }
}
