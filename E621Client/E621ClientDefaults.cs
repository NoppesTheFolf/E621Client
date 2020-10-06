using System;

namespace Noppes.E621
{
    public partial class E621Client
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
        public static TimeSpan MinimumRequestInterval { get; } = TimeSpan.FromMilliseconds(MinimumRequestIntervalInMilliseconds);

        /// <summary>
        /// The recommended interval in milliseconds between each request as per the e621 docs.
        /// </summary>
        public static readonly int RecommendedRequestIntervalInMilliseconds = 1000;

        /// <summary>
        /// The recommended interval between each request as per the e621 docs.
        /// </summary>
        public static TimeSpan RecommendedRequestInterval { get; } = TimeSpan.FromMilliseconds(RecommendedRequestIntervalInMilliseconds);

        /// <summary>
        /// Recommended number of maximum concurrent connections allowed to the e621 servers.
        /// </summary>
        public static int DefaultMaximumConnections { get; } = 2;

        /// <summary>
        /// Maximum number of maximum concurrent connections allowed to the e621 servers.
        /// </summary>
        public static int MaximumConnectionsLimit { get; } = 4;

        /// <summary>
        /// The minimum allowed amount of time in milliseconds before a request will be considered
        /// as timed out.
        /// </summary>
        public static readonly int MinimumRequestTimeoutInMilliseconds = 5000;

        /// <summary>
        /// The minimum allowed amount of time before a request will be considered as timed out.
        /// </summary>
        public static TimeSpan MinimumRequestTimeout { get; } = TimeSpan.FromMilliseconds(MinimumRequestTimeoutInMilliseconds);

        /// <summary>
        /// The default amount of time in milliseconds before a request will be considered as timed out.
        /// </summary>
        public static readonly int RecommendedRequestTimeoutInMilliseconds = 15000;

        /// <summary>
        /// The default amount of time before a request will be considered as timed out.
        /// </summary>
        public static TimeSpan RecommendedRequestTimeout { get; } = TimeSpan.FromMilliseconds(RecommendedRequestTimeoutInMilliseconds);

        /// <summary>
        /// The default imageboard information is retrieved from.
        /// </summary>
        public static Imageboard DefaultImageboard { get; } = Imageboard.E621;
    }
}
