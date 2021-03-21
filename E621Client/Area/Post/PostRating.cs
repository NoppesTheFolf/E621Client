using System;

namespace Noppes.E621
{
    /// <summary>
    /// The ratings a post can have. See <see href="https://e621.net/wiki_pages/8717">this wiki
    /// page</see> for more information.
    /// </summary>
    public enum PostRating
    {
        /// <summary>
        /// Posts which are considered SFW. See <see href="https://e621.net/wiki_pages/8717">this
        /// wiki page</see> for specifics.
        /// </summary>
        Safe,
        /// <summary>
        /// Posts which depict mature content, so NSFW. See <see
        /// href="https://e621.net/wiki_pages/8717">this wiki page</see> for specifics.
        /// </summary>
        Questionable,
        /// <summary>
        /// We all know what this means :) See https://e621.net/wiki_pages/8717 for specifics.
        /// </summary>
        Explicit
    }

    internal static class PostRatingHelper
    {
        public static PostRating FromAbbreviation(string value)
        {
            return value switch
            {
                "s" => PostRating.Safe,
                "q" => PostRating.Questionable,
                "e" => PostRating.Explicit,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static string ToAbbreviation(this PostRating rating)
        {
            return rating switch
            {
                PostRating.Safe => "s",
                PostRating.Questionable => "q",
                PostRating.Explicit => "e",
                _ => throw new ArgumentOutOfRangeException(nameof(rating))
            };
        }
    }
}
