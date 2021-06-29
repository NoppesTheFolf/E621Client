using Dawn;
using Flurl.Http;
using Noppes.E621.Extensions;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Noppes.E621
{
    public partial class E621Client
    {
        /// <inheritdoc/>
        public Task<Pool?> GetPoolAsync(int id)
        {
            return RequestAsync($"/pools/{id}.json", request => request.GetJsonAsync<Pool>(false, HttpStatusCode.NotFound));
        }

        /// <inheritdoc/>
        public Task<ICollection<Pool>> GetPoolsAsync(int id, Position position, string? name = null, string? description = null, IEnumerable<int>? ids = null,
            int? creatorId = null, string? creatorName = null, bool? isActive = null, bool? isDeleted = null,
            PoolCategory? category = null, int? limit = null)
        {
            return GetPoolsAsync(name, description, ids, creatorId, creatorName, isActive, isDeleted, category, null, limit, id, position);
        }

        /// <inheritdoc/>
        public Task<ICollection<Pool>> GetPoolsAsync(int page = 1, string? name = null, string? description = null,
            IEnumerable<int>? ids = null, int? creatorId = null, string? creatorName = null, bool? isActive = null,
            bool? isDeleted = null, PoolCategory? category = null, PoolOrder order = PoolOrder.UpdatedAt, int? limit = null)
        {
            return GetPoolsAsync(name, description, ids, creatorId, creatorName, isActive, isDeleted, category, order, limit, page, null);
        }

        private Task<ICollection<Pool>> GetPoolsAsync(string? name, string? description, IEnumerable<int>? ids, int? creatorId,
            string? creatorName, bool? isActive, bool? isDeleted, PoolCategory? category, PoolOrder? order, int? limit, int? page, Position? position)
        {
            Guard.Argument(limit, nameof(limit)).InRange(1, E621Constants.PoolsMaximumLimit);

            Page.Validate(page, nameof(page), E621Constants.PoolsMaximumPage, position);

            return RequestAsync("/pools.json", request => request
                .SetPagination(page, position)
                .SetQueryParam("limit", limit)
                .SetQueryParam("search[name_matches]", name)
                .SetQueryParam("search[description_matches]", description)
                .SetQueryParam("search[id]", ids == null ? null : string.Join(",", ids))
                .SetQueryParam("search[creator_name]", creatorName)
                .SetQueryParam("search[creator_id]", creatorId)
                .SetQueryParam("search[is_active]", isActive)
                .SetQueryParam("search[is_deleted]", isDeleted)
                .SetQueryParam("search[category]", category?.ToApiParameter())
                .SetQueryParam("search[order]", order?.ToApiParameter())
                .GetJsonAsync<ICollection<Pool>>());
        }
    }
}
