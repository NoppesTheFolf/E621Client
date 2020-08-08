using System;

namespace Noppes.E621
{
    /// <summary>
    /// The ratings a post can have.
    /// </summary>
    public enum PostRating
    {
        Safe,
        Questionable,
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
