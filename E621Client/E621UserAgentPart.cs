using Dawn;
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

        public E621UserAgentPart(string productName, string productVersion, string username, string platform, string? locationUrl = null)
        {
            ProductName = Guard.Argument(productName, nameof(productName)).NotNull().NotWhiteSpace();
            ProductVersion = Guard.Argument(productVersion, nameof(productVersion)).NotNull().NotWhiteSpace();
            Username = Guard.Argument(username, nameof(username)).NotNull().NotWhiteSpace();
            Platform = Guard.Argument(platform, nameof(platform)).NotNull().NotWhiteSpace();

            if (locationUrl == null)
                return;

            var locationUri = new Uri(locationUrl, UriKind.Absolute);
            Guard.Argument(locationUri, nameof(locationUrl)).Http();

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
