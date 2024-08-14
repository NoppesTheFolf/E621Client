using System;

namespace Noppes.E621
{
    /// <summary>
    /// This class contains a bunch of constants related to e621. Constants like the maximum number
    /// of posts which can be retrieved in a single call or the minimum interval between requests
    /// are defined here.
    /// </summary>
    public static partial class E621Constants
    {
        /// <summary>
        /// The minimum allowed interval in milliseconds between each request.
        /// </summary>
        public static readonly int MinimumRequestIntervalInMilliseconds = 500;

        /// <summary>
        /// The minimum allowed interval between each request. You should only use this value if
        /// you're developing a time-sensitive application. You also shouldn't use this if you're
        /// making lots of requests over a sustained period of time.
        /// </summary>
        public static TimeSpan MinimumRequestInterval =>
            TimeSpan.FromMilliseconds(MinimumRequestIntervalInMilliseconds);

        /// <summary>
        /// The recommended interval in milliseconds between each request as per the e621 docs.
        /// </summary>
        public static readonly int RecommendedRequestIntervalInMilliseconds = 1000;

        /// <summary>
        /// The recommended interval between each request as per the e621 docs.
        /// </summary>
        public static TimeSpan RecommendedRequestInterval =>
            TimeSpan.FromMilliseconds(RecommendedRequestIntervalInMilliseconds);

        /// <summary>
        /// Recommended number of maximum concurrent connections allowed to the e621 servers.
        /// </summary>
        public static readonly int DefaultMaximumConnections = 2;

        /// <summary>
        /// Maximum number of maximum concurrent connections allowed to the e621 servers.
        /// </summary>
        public static readonly int MaximumConnectionsLimit = 4;

        /// <summary>
        /// The minimum allowed amount of time in milliseconds before a request will be considered
        /// as timed out.
        /// </summary>
        public static readonly int MinimumRequestTimeoutInMilliseconds = 5000;

        /// <summary>
        /// The minimum allowed amount of time before a request will be considered as timed out.
        /// </summary>
        public static TimeSpan MinimumRequestTimeout =>
            TimeSpan.FromMilliseconds(MinimumRequestTimeoutInMilliseconds);

        /// <summary>
        /// The default amount of time in milliseconds before a request will be considered as timed out.
        /// </summary>
        public static readonly int RecommendedRequestTimeoutInMilliseconds = 15000;

        /// <summary>
        /// The default amount of time before a request will be considered as timed out.
        /// </summary>
        public static TimeSpan RecommendedRequestTimeout =>
            TimeSpan.FromMilliseconds(RecommendedRequestTimeoutInMilliseconds);

        /// <summary>
        /// The default imageboard information is retrieved from.
        /// </summary>
        [Obsolete("The use of Imageboard is no longer supported, use DefaultBaseUrl, E621BaseUrl or E921BaseUrl instead.")]
        public static readonly Imageboard DefaultImageboard = Imageboard.E621;

        /// <summary>
        /// The URL to the E621 imageboard.
        /// </summary>
        public static readonly Uri E621BaseUrl = new Uri("https://e621.net");
        
        /// <summary>
        /// The URL to the E926 imageboard.
        /// </summary>
        public static readonly Uri E926BaseUrl = new Uri("https://e926.net");
        
        /// <summary>
        /// The default URL for the imageboard information is retrieved from.
        /// </summary>
        public static readonly Uri DefaultBaseUrl = E621BaseUrl;
    }
}