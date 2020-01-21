using System;

namespace Noppes.E621
{
    public partial class E621Client
    {
        /// <summary>
        /// The minimum allowed interval between each request. You should only
        /// use this value if you're developing a time-sensitive application.
        /// You also shouldn't use this if you're making lots of requests over a sustained period of time.
        /// </summary>
        public static TimeSpan MinimumRequestInterval { get; } = TimeSpan.FromMilliseconds(500);

        /// <summary>
        /// The recommended interval between each request as per the e621 docs.
        /// </summary>
        public static TimeSpan RecommendedRequestInterval { get; } = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Recommended number of maximum concurrent connections allowed to the e621 servers.
        /// </summary>
        public static int DefaultMaximumConnections { get; } = 2;

        /// <summary>
        /// Maximum number of maximum concurrent connections allowed to the e621 servers.
        /// </summary>
        public static int MaximumConnectionsLimit { get; } = 4;

        /// <summary>
        /// The minimum allowed amount of time before a request will be considered as timed out.
        /// </summary>
        public static TimeSpan MinimumRequestTimeout { get; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// The default amount of time before a request will be considered as timed out.
        /// </summary>
        public static TimeSpan RecommendedRequestTimeout { get; } = TimeSpan.FromSeconds(15);

        /// <summary>
        /// The default imageboard information is retrieved from.
        /// </summary>
        public static Imageboard DefaultImageboard { get; } = Imageboard.E621;
    }
}
