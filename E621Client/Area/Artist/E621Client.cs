using Dawn;
using Flurl.Http;
using Noppes.E621.Extensions;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Implementation for the artists area of e621
    public partial class E621Client
    {
        /// <inheritdoc/>
        public Task<ICollection<Artist>> GetArtistsAsync(int page = 1, string? name = null, string? url = null, string? creatorName = null,
            bool? isActive = null, bool? isBanned = null, bool? hasTag = null, ArtistOrder order = ArtistOrder.CreatedAt, int? limit = null)
        {
            return GetArtistsAsync(name, url, creatorName, isActive, isBanned, hasTag, order, limit, page, null);
        }

        /// <inheritdoc/>
        public Task<ICollection<Artist>> GetArtistsAsync(int id, Position position, string? name = null, string? url = null, string? creatorName = null,
            bool? isActive = null, bool? isBanned = null, bool? hasTag = null, ArtistOrder order = ArtistOrder.CreatedAt, int? limit = null)
        {
            return GetArtistsAsync(name, url, creatorName, isActive, isBanned, hasTag, order, limit, id, position);
        }

        private Task<ICollection<Artist>> GetArtistsAsync(string? name, string? url, string? creatorName,
            bool? isActive, bool? isBanned, bool? hasTag, ArtistOrder? order, int? limit, int? page, Position? position)
        {
            Guard.Argument(limit, nameof(limit)).InRange(1, E621Constants.PoolsMaximumLimit);
            Page.Validate(page, nameof(page), E621Constants.PoolsMaximumPage, position);

            return RequestAsync("/artists.json", request => request
                .SetPagination(page, position)
                .SetQueryParam("limit", limit)
                .SetQueryParam("search[any_name_matches]", name)
                .SetQueryParam("search[url_matches]", url)
                .SetQueryParam("search[creator_name]", creatorName)
                .SetQueryParam("search[is_active]", isActive)
                .SetQueryParam("search[is_banned]", isBanned)
                .SetQueryParam("search[has_tag]", hasTag)
                .SetQueryParam("search[order]", order?.ToApiParameter())
                .GetJsonAsync<ICollection<Artist>>());
        }

        /// <inheritdoc/>
        public Task<Artist?> GetArtistAsync(int id)
        {
            return RequestAsync($"/artists/{id}.json",
                request => request.GetJsonAsync<Artist>(true, HttpStatusCode.NotFound));
        }
    }
}
