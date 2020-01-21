using Newtonsoft.Json;

namespace Noppes.E621
{
    internal class LoginResult
    {
        public bool IsSuccess => Username != null && ApiKey != null;

        [JsonProperty("name")]
        public string? Username { get; set; }

        [JsonProperty("password_hash")]
        public string? ApiKey { get; set; }
    }
}
