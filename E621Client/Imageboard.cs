using System;

namespace Noppes.E621
{
    /// <summary>
    /// Imageboards of which data can be retrieved from.
    /// </summary>
    public enum Imageboard
    {
        /// <summary>
        /// Use e921 as source for API requests.
        /// </summary>
        E621,
        /// <summary>
        /// Use e921 as source for API requests.
        /// </summary>
        E926
    }

    internal static class ImageboardExtensions
    {
        public const string E621Url = "https://e621.net";
        public const string E921Url = "https://e926.net";

        /// <summary>
        /// Maps the imageboard to a base URL that can be for HTTP clients.
        /// </summary>
        public static string AsBaseUrl(this Imageboard imageboard)
        {
            return imageboard switch
            {
                Imageboard.E621 => E621Url,
                Imageboard.E926 => E921Url,
                _ => throw new ArgumentOutOfRangeException(nameof(imageboard))
            };
        }
    }
}
