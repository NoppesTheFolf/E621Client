using System;

namespace Noppes.E621
{
    /// <summary>
    /// Imageboards of which data can be retrieved from.
    /// </summary>
    public enum Imageboard
    {
        E621,
        E926
    }

    internal static class ImageboardExtensions
    {
        /// <summary>
        /// Maps the imageboard to a base URL that can be for HTTP clients.
        /// </summary>
        public static string AsBaseUrl(this Imageboard imageboard)
        {
            return imageboard switch
            {
                Imageboard.E621 => "https://e621.net",
                Imageboard.E926 => "https://e926.net",
                _ => throw new ArgumentOutOfRangeException(nameof(imageboard))
            };
        }
    }
}
