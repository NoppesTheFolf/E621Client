using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Text.RegularExpressions;

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

        private string _url = null!;
        /// <summary>
        /// The URL retrieved from the API. Also see the <see cref="Location"/> property. Note that
        /// this URL contains user provided information and might be invalid.
        /// </summary>
        [JsonProperty("url")]
        public string Url
        {
            get => _url;
            set
            {
                _url = value;

                var url = Regex.IsMatch(Url, "[A-z]+:\\/\\/") ? Url : $"https://{Url}";
                Location = Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri) ? uri : null;
            }
        }

        /// <summary>
        /// The actual URL itself at which the artist can be found. Might be null if the user
        /// provided URL is invalid. It will try to append the HTTPS scheme if the user did not
        /// provide a scheme.
        /// </summary>
        public Uri? Location { get; set; }

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
