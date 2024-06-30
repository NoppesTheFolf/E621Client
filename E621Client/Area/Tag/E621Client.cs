using Dawn;
using Flurl.Http;
using Noppes.E621.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Noppes.E621
{
    public partial class E621Client
    {
        /// <inheritdoc/>
        public Task<Tag?> GetTagAsync(int id)
        {
            return RequestAsync($"/tags/{id}.json", request => request.GetJsonAsync<Tag>(true, HttpStatusCode.NotFound));
        }

        /// <inheritdoc/>
        public async Task<Tag?> GetTagAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await Task.FromResult<Tag?>(null).ConfigureAwait(false);

            return (await GetTagsByNamesAsync(new[] { name }, hideEmpty: false).ConfigureAwait(false)).FirstOrDefault();
        }

        #region Tags without filter

        /// <inheritdoc/>
        public Task<ICollection<Tag>> GetTagsAsync(int id, Position position, int? limit = null, TagCategory? category = null, bool hideEmpty = true, bool? hasWiki = null)
        {
            return GetTagsWithParameterAsync(null, id, position, limit, category, null, hideEmpty, hasWiki);
        }

        /// <inheritdoc/>
        public Task<ICollection<Tag>> GetTagsAsync(int? page = null, int? limit = null, TagCategory? category = null, TagOrder? order = null, bool hideEmpty = true, bool? hasWiki = null)
        {
            return GetTagsWithParameterAsync(null, page, null, limit, category, order, hideEmpty, hasWiki);
        }

        #endregion

        #region Tags filter by name

        /// <inheritdoc/>
        public Task<ICollection<Tag>> GetTagsByNamesAsync(IEnumerable<string> names, int? page = null, int? limit = null, TagCategory? category = null, TagOrder? order = null, bool hideEmpty = true, bool? hasWiki = null)
        {
            return GetTagsByNamesAsync(names, page, null, limit, category, order, hideEmpty, hasWiki);
        }

        private Task<ICollection<Tag>> GetTagsByNamesAsync(IEnumerable<string> names, int? page, Position? position, int? limit, TagCategory? category, TagOrder? order, bool? hideEmpty, bool? hasWiki)
        {
            var filteredNames = names
                .Select(name => name.Trim())
                .Where(name => !string.IsNullOrWhiteSpace(name));

            string namesQuery = string.Join(',', filteredNames);

            return GetTagsWithParameterAsync(("search[name]", namesQuery), page, position, limit, category, order, hideEmpty, hasWiki);
        }

        #endregion

        #region Tags filter with query

        /// <inheritdoc/>
        public Task<ICollection<Tag>> GetTagsByNamesAsync(string query, int? page = null, int? limit = null, TagCategory? category = null, TagOrder? order = null, bool? hideEmpty = null, bool? hasWiki = null)
        {
            return GetTagsByNamesAsync(query, page, null, limit, category, order, hideEmpty, hasWiki);
        }

        private Task<ICollection<Tag>> GetTagsByNamesAsync(string query, int? id, Position? position, int? limit, TagCategory? category, TagOrder? order, bool? hideEmpty, bool? hasWiki)
        {
            return GetTagsWithParameterAsync(("search[name_matches]", query), id, position, limit, category, order, hideEmpty, hasWiki);
        }

        #endregion

        private Task<ICollection<Tag>> GetTagsWithParameterAsync((string name, string? value)? param, int? page, Position? position, int? limit, TagCategory? category, TagOrder? order, bool? hideEmpty, bool? hasWiki)
        {
            Guard.Argument(limit, nameof(limit)).InRange(1, E621Constants.TagsMaximumLimit);

            Page.Validate(page, nameof(page), E621Constants.TagsMaximumPage, position);

            return RequestAsync("/tags.json", request =>
            {
                if (param.HasValue)
                    request = request.SetQueryParam(param.Value.name, param.Value.value);

                return request
                    .SetQueryParam("search[category]", category?.AsUnderlyingType())
                    .SetQueryParam("search[order]", order?.ToApiParameter())
                    .SetQueryParam("search[hide_empty]", hideEmpty?.ToString())
                    .SetQueryParam("search[has_wiki]", hasWiki?.ToString())
                    .SetQueryParam("limit", limit)
                    .SetPagination(page, position)
                    .GetJsonAsync(token =>
                    {
                        var tagsToken = token.SelectToken("tags");

                        return tagsToken == null ? token.ToObject<ICollection<Tag>>() : tagsToken.ToObject<ICollection<Tag>>();
                    });
            });
        }
    }
}
