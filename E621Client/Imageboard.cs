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
        E926,
        
        /// <summary>
        /// Use e6ai as source for API requests.
        /// </summary>
        E6AI
    }

    internal static class ImageboardExtensions
    {
        private const string E621BaseUrlRegistrableDomain = "e621.net";
        private const string E621BaseUrl = "https://" + E621BaseUrlRegistrableDomain;

        private const string E921BaseUrlRegistrableDomain = "e926.net";
        private const string E921BaseUrl = "https://" + E921BaseUrlRegistrableDomain;

        private const string E6AIBaseUrlRegistrableDomain = "e6ai.net";
        private const string E6AIBaseUrl = "https://" + E6AIBaseUrlRegistrableDomain;

        /// <summary>
        /// Maps the imageboard to a base URL that can be for HTTP clients.
        /// </summary>
        public static (string registrableDomain, string baseUrl) AsBaseUrl(this Imageboard imageboard)
        {
            return imageboard switch
            {
                Imageboard.E621 => (E621BaseUrlRegistrableDomain, E621BaseUrl),
                Imageboard.E926 => (E921BaseUrlRegistrableDomain, E921BaseUrl),
                Imageboard.E6AI => (E6AIBaseUrlRegistrableDomain, E6AIBaseUrl),
                _ => throw new ArgumentOutOfRangeException(nameof(imageboard))
            };
        }
    }
}