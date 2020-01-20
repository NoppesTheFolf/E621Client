using Noppes.E621.Extensions;
using System;
using System.Text;

namespace Noppes.E621
{
    /// <summary>
    /// A part of a User-Agent.
    /// </summary>
    internal class E621UserAgentPart
    {
        public string ProductName { get; }

        public string ProductVersion { get; }

        public string Username { get; }

        public string Platform { get; }

        public string? LocationUrl { get; }

        public E621UserAgentPart(string productName, string productVersion, string username,
            string platform, string? locationUrl = null)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentNullOrWhiteSpaceException(nameof(productName));

            if (string.IsNullOrWhiteSpace(productVersion))
                throw new ArgumentNullOrWhiteSpaceException(nameof(productVersion));

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullOrWhiteSpaceException(nameof(platform));

            if (string.IsNullOrWhiteSpace(platform))
                throw new ArgumentNullOrWhiteSpaceException(nameof(platform));

            ProductName = productName;
            ProductVersion = productVersion;
            Username = username;
            Platform = platform;

            if (locationUrl == null)
                return;

            Uri locationUri = new Uri(locationUrl, UriKind.Absolute);
            locationUri.EnsureHttpOrHttps();

            LocationUrl = locationUrl;
        }

        public void AppendString(StringBuilder stringBuilder)
        {
            stringBuilder.Append($"{ProductName}/{ProductVersion} (by {Username} on {Platform}");

            if (!string.IsNullOrWhiteSpace(LocationUrl))
                stringBuilder.Append($" at {LocationUrl}");

            stringBuilder.Append(")");
        }
    }
}
