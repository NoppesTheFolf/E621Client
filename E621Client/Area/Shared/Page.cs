using Dawn;
using Flurl.Http;

namespace Noppes.E621
{
    /// <summary>
    /// Represents the way a "page" works at e621.
    /// </summary>
    internal class Page
    {
        public int? Index { get; set; }

        public Position? Position { get; set; }

        public Page(int? index, Position? position)
        {
            Index = index;
            Position = position;
        }

        public string? AsParameterValue()
        {
            if (Index == null)
                return null;

            return Position == null
                ? Index.ToString()
                : ((Position)Position).ToIdentifier() + Index.ToString();
        }

        public static void Validate(int? index, string indexName, int maxIndex, Position? position)
        {
            if (position == null)
                Guard.Argument(index, indexName).InRange(1, maxIndex);
        }
    }

    internal static class PageExtensions
    {
        public static IFlurlRequest SetPagination(this IFlurlRequest request, int? page, Position? position) =>
            request.SetPagination(new Page(page, position));

        public static IFlurlRequest SetPagination(this IFlurlRequest request, Page page) =>
            request.SetQueryParam("page", page.AsParameterValue());
    }
}
