using System;

namespace Noppes.E621.Extensions
{
    /// <summary>
    /// Adds extra functionality to the <see cref="Uri"/> class.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Ensure the URI has a HTTP or HTTPS scheme.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static void EnsureHttpOrHttps(this Uri uri)
        {
            if (!uri.IsHttpOrHttps())
                throw new ArgumentException($"The URI does not have a HTTP or HTTPS scheme.", nameof(uri));
        }

        public static bool IsHttpOrHttps(this Uri uri) => uri.IsHttp() || uri.IsHttps();

        public static bool IsHttp(this Uri uri) => uri.Scheme.ToLowerInvariant() == "http";

        public static bool IsHttps(this Uri uri) => uri.Scheme.ToLowerInvariant() == "https";
    }
}
