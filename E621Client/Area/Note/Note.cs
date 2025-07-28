using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Noppes.E621
{
    public class Note
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
        /// The version of the note
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; }

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
        /// The creator's name.
        /// </summary>
        [JsonProperty("creator_name"), JsonConverter(typeof(EmptyStringConverter))]
        public string? CreatorName { get; set; }

        /// <summary>
        /// The text of the note.
        /// </summary>
        [JsonProperty("body"), JsonConverter(typeof(EmptyStringConverter))]
        public string? Body { get; set; }

        /// <summary>
        /// Whether the note is active.
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// The X position of the note on the image, in pixels.
        /// </summary>
        [JsonProperty("x")]
        public int X { get; set; }

        /// <summary>
        /// The Y position of the note on the image, in pixels.
        /// </summary>
        [JsonProperty("y")]
        public int Y { get; set; }

        /// <summary>
        /// The width of the note, in pixels.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// The height of the note, in pixels.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
