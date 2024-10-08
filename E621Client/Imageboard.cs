﻿using System;

namespace Noppes.E621
{
    /// <summary>
    /// Imageboards of which data can be retrieved from.
    /// </summary>
    [Obsolete("Using the Imageboard is no longer recommended. Use the Url overload of WithBaseUrl instead.")]
    public enum Imageboard
    {
        /// <summary>
        /// Use e921 as source for API requests.
        /// </summary>
        E621,
        /// <summary>
        /// Use e926 as source for API requests.
        /// </summary>
        E926
    }

    internal static class ImageboardExtensions
    {
        private const string E621BaseUrlRegistrableDomain = "e621.net";
        private const string E621BaseUrl = "https://" + E621BaseUrlRegistrableDomain;

        private const string E921BaseUrlRegistrableDomain = "e926.net";
        private const string E921BaseUrl = "https://" + E921BaseUrlRegistrableDomain;

        /// <summary>
        /// Maps the imageboard to a base URL that can be for HTTP clients.
        /// </summary>
        [Obsolete("No longer supported to create a client with an image board. Use the WithBaseUrl(Uri) in the builder instead.")]
        public static (string registrableDomain, string baseUrl) AsBaseUrl(this Imageboard imageboard)
        {
            return imageboard switch
            {
                Imageboard.E621 => (E621BaseUrlRegistrableDomain, E621BaseUrl),
                Imageboard.E926 => (E921BaseUrlRegistrableDomain, E921BaseUrl),
                _ => throw new ArgumentOutOfRangeException(nameof(imageboard))
            };
        }
    }
}