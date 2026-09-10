using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;

namespace Noppes.E621
{
    /// <summary>
    /// Contains information about a database export.
    /// </summary>
    public class DbExport
    {
        /// <summary>
        /// The name of the database export. This is one of the values in <see cref="DbExportType"/>.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The name of the database export file on the server.
        /// </summary>
        [JsonProperty("file_name")]
        public string FileName { get; set; } = null!;

        /// <summary>
        /// The size of the database export file in bytes.
        /// </summary>
        [JsonProperty("file_size")]
        public long FileSize { get; set; }

        /// <summary>
        /// The SHA-256 checksum of the database export file.
        /// </summary>
        [JsonProperty("checksum")]
        public string Checksum { get; set; } = null!;

        /// <summary>
        /// When the database got exported.
        /// </summary>
        [JsonProperty("updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The URL where the database export file can be downloaded.
        /// </summary>
        [JsonProperty("url"), JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; } = null!;
    }
}
