using System;
using System.Collections.Generic;
using System.Linq;

namespace Noppes.E621
{
    public static class E621Constants
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
        public static TimeSpan MinimumRequestInterval { get; } =
            TimeSpan.FromMilliseconds(MinimumRequestIntervalInMilliseconds);

        /// <summary>
        /// The recommended interval in milliseconds between each request as per the e621 docs.
        /// </summary>
        public static readonly int RecommendedRequestIntervalInMilliseconds = 1000;

        /// <summary>
        /// The recommended interval between each request as per the e621 docs.
        /// </summary>
        public static TimeSpan RecommendedRequestInterval { get; } =
            TimeSpan.FromMilliseconds(RecommendedRequestIntervalInMilliseconds);

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
        public static TimeSpan MinimumRequestTimeout { get; } =
            TimeSpan.FromMilliseconds(MinimumRequestTimeoutInMilliseconds);

        /// <summary>
        /// The default amount of time in milliseconds before a request will be considered as timed out.
        /// </summary>
        public static readonly int RecommendedRequestTimeoutInMilliseconds = 15000;

        /// <summary>
        /// The default amount of time before a request will be considered as timed out.
        /// </summary>
        public static TimeSpan RecommendedRequestTimeout { get; } =
            TimeSpan.FromMilliseconds(RecommendedRequestTimeoutInMilliseconds);

        /// <summary>
        /// The default imageboard information is retrieved from.
        /// </summary>
        public static Imageboard DefaultImageboard { get; } = Imageboard.E621;

        /// <summary>
        /// The image formats IQDB supports. This <see cref="IReadOnlyDictionary{TKey,TValue}"/>
        /// contains both the abbreviation of the image format (JPEG, PNG, GIF) and their
        /// corresponding file extensions. However, you are unlikely to need this. Take a look at
        /// <see cref="IqdbAllowedFormatNames"/> and <see cref="IqdbAllowedFormatExtensions"/> first.
        /// </summary>
        public static IReadOnlyDictionary<string, string[]> IqdbAllowedFormats = new Dictionary<string, string[]>
        {
            {"JPEG", new[] {"jpg", "jpeg"}},
            {"PNG", new[] {"png"}},
            {"GIF", new[] {"gif"}}
        };
    
        /// <summary>
        /// The names of the image formats IQDB supports.
        /// </summary>
        public static IReadOnlyCollection<string> IqdbAllowedFormatNames = IqdbAllowedFormats
            .Select(iaif => iaif.Key)
            .ToHashSet();

        /// <summary>
        /// The file extensions of the image formats IQDB supports.
        /// </summary>
        public static IReadOnlyCollection<string> IqdbAllowedFormatExtensions = IqdbAllowedFormats
            .SelectMany(iaif => iaif.Value)
            .Select(e => '.' + e)
            .ToHashSet();

        /// <summary>
        /// The maximum possible number of favorites retrieved in a single call to <see cref="GetFavoritesAsync"/>.
        /// </summary>
        public static int FavoritesMaximum { get; } = 75;

        /// <summary>
        /// The maximum number of posts which can be retrieved in a single call to <see cref="GetPostsAsync"/>.
        /// </summary>
        public static int PostsMaximumLimit { get; } = 320;

        /// <summary>
        /// The maximum allowed page number when making a call to <see cref="GetPostsAsync"/>.
        /// </summary>
        public static int PostsMaximumPage { get; } = 750;

        /// <summary>
        /// The maximum number of tags which can be searched for in a single call to <see cref="GetPostsAsync"/>.
        /// </summary>
        public static int PostsMaximumTagSearchCount { get; } = 6;

        /// <summary>
        /// The maximum number of tags which can be retrieved in a single call to one of the
        /// GetTagsAsync overloads.
        /// </summary>
        public static int TagsMaximumLimit { get; } = 1000;

        /// <summary>
        /// The maximum allowed page number when making a call one of the GetTagsAsync overloads.
        /// </summary>
        public static int TagsMaximumPage { get; } = 750;
    }
}