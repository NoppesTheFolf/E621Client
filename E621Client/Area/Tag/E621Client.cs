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
        /// <summary>
        /// The maximum number of tags which can be retrieved in a single call to one of the
        /// GetTagsAsync overloads.
        /// </summary>
        public static int TagsMaximumLimit { get; } = 1000;

        /// <summary>
        /// The maximum allowed page number when making a call one of the GetTagsAsync overloads.
        /// </summary>
        public static int TagsMaximumPage { get; } = 750;

        /// <summary>
        /// Retrieve a tag by its ID. Returns null when no tag matches the given ID.
        /// </summary>
        /// <param name="id">The ID of the tag.</param>
        public Task<Tag?> GetTagAsync(int id)
        {
            return RequestAsync($"/tags/{id}.json", request => request.GetJsonAsync<Tag>(true, HttpStatusCode.NotFound));
        }

        /// <summary>
        /// Retrieve a tag by its name. Returns null when no tag matches the name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        public async Task<Tag?> GetTagAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await Task.FromResult<Tag?>(null).ConfigureAwait(false);

            return (await GetTagsByNames(new[] { name }, hideEmpty: false).ConfigureAwait(false)).FirstOrDefault();
        }

        #region Tags without filter

        /// <summary>
        /// Retrieves a collection of tags based on the given parameters.
        /// <para>
        /// Be aware that there is a limit on the number of tags which can be retrieved with a
        /// single call. This limit is defined at <see cref="TagsMaximumLimit"/>. Exceeding this
        /// limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the tags by specifying both an tag ID and a position. The
        /// position parameter defines the position of the returned tags relative to the given tag
        /// ID. Let's take tag with ID 1000 as an example. Passing this ID in combination with <see
        /// cref="Position.Before"/> will cause the tags 999, 998, 997, etc. to be retrieved. Using
        /// <see cref="Position.After"/> will retrieve the tags 1001, 1002, 1003, etc. You should
        /// use this method if you don't need pagination or need to avoid the limit pagination comes
        /// with it. Moreover, this is the most efficient way to navigate through tags.
        /// </para>
        /// </summary>
        /// <param name="id">
        /// ID of the tag you want the retrieved tags the be relatively positioned to.
        /// </param>
        /// <param name="position">Relative position to the given tag ID.</param>
        /// <param name="limit">The maximum number of tags returned in a single request.</param>
        /// <param name="category">The category the retrieved tags should be in.</param>
        /// <param name="hideEmpty">
        /// Whether or not to include tags that have no posts associated with them.
        /// </param>
        /// <param name="hasWiki">Whether or not to include tags that have a page at e621's wiki.</param>
        public Task<ICollection<Tag>> GetTagsAsync(int id, Position position, int? limit = null, TagCategory? category = null, bool hideEmpty = true, bool? hasWiki = null)
        {
            return GetTagsWithParameterAsync(null, id, position, limit, category, null, hideEmpty, hasWiki);
        }

        /// <summary>
        /// Retrieves a collection of tags based on the given parameters.
        /// <para>
        /// Be aware that there is a limit on the number of tags which can be retrieved with a
        /// single call. This limit is defined at <see cref="TagsMaximumLimit"/>. Exceeding this
        /// limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the tags by specifying a page number. There is, however, a
        /// limit to the value. This limit is defined in <see cref="TagsMaximumPage"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// </summary>
        /// <param name="page">Pagination, number of the page.</param>
        /// <param name="limit">The maximum number of tags returned in a single request.</param>
        /// <param name="category">The category the retrieved tags should be in.</param>
        /// <param name="order">The order in which the retrieved tags should be.</param>
        /// <param name="hideEmpty">
        /// Whether or not to include tags that have no posts associated with them.
        /// </param>
        /// <param name="hasWiki">Whether or not to include tags that have a page at e621's wiki.</param>
        public Task<ICollection<Tag>> GetTagsAsync(int? page = null, int? limit = null, TagCategory? category = null, TagOrder? order = null, bool hideEmpty = true, bool? hasWiki = null)
        {
            return GetTagsWithParameterAsync(null, page, null, limit, category, order, hideEmpty, hasWiki);
        }

        #endregion

        #region Tags filter by name

        /// <summary>
        /// <para>
        /// Retrieves a collection of tags by name and the other given parameters. If a given name
        /// doesn't exist, it won't be in the returned collection.
        /// </para>
        /// <para>
        /// Be aware that there is a limit on the number of tags which can be retrieved with a
        /// single call. This limit is defined at <see cref="TagsMaximumLimit"/>. Exceeding this
        /// limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the tags by specifying a page number. There is, however, a
        /// limit to the value. This limit is defined in <see cref="TagsMaximumPage"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// </summary>
        /// <param name="names">Names of the tags you want to retrieve.</param>
        /// <param name="page">Pagination, number of the page.</param>
        /// <param name="limit">The maximum number of tags returned in a single request.</param>
        /// <param name="category">The category the retrieved tags should be in.</param>
        /// <param name="order">The order in which the retrieved tags should be.</param>
        /// <param name="hideEmpty">
        /// Whether or not to include tags that have no posts associated with them.
        /// </param>
        /// <param name="hasWiki">Whether or not to include tags that have a page at e621's wiki.</param>
        public Task<ICollection<Tag>> GetTagsByNames(IEnumerable<string> names, int? page = null, int? limit = null, TagCategory? category = null, TagOrder? order = null, bool hideEmpty = true, bool? hasWiki = null)
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

        /// <summary>
        /// <para>Retrieves a collection of tags using a query and the other given parameters.</para>
        /// <para>
        /// Be aware that there is a limit on the number of tags which can be retrieved with a
        /// single call. This limit is defined at <see cref="TagsMaximumLimit"/>. Exceeding this
        /// limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the tags by specifying a page number. There is, however, a
        /// limit to the value. This limit is defined in <see cref="TagsMaximumPage"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// </summary>
        /// <param name="query">The query used to filter the collection with.</param>
        /// <param name="page">Pagination, number of the page.</param>
        /// <param name="limit">The maximum number of tags returned in a single request.</param>
        /// <param name="category">The category the retrieved tags should be in.</param>
        /// <param name="order">The order in which the retrieved tags should be.</param>
        /// <param name="hideEmpty">
        /// Whether or not to include tags that have no posts associated with them.
        /// </param>
        /// <param name="hasWiki">Whether or not to include tags that have a page at e621's wiki.</param>
        public Task<ICollection<Tag>> GetTagsByNames(string query, int? page = null, int? limit = null, TagCategory? category = null, TagOrder? order = null, bool? hideEmpty = null, bool? hasWiki = null)
        {
            return GetTagsByNames(query, page, null, limit, category, order, hideEmpty, hasWiki);
        }

        private Task<ICollection<Tag>> GetTagsByNames(string query, int? id, Position? position, int? limit, TagCategory? category, TagOrder? order, bool? hideEmpty, bool? hasWiki)
        {
            return GetTagsWithParameterAsync(("search[name_matches]", query), id, position, limit, category, order, hideEmpty, hasWiki);
        }

        #endregion

        private Task<ICollection<Tag>> GetTagsWithParameterAsync((string name, string? value)? param, int? page, Position? position, int? limit, TagCategory? category, TagOrder? order, bool? hideEmpty, bool? hasWiki)
        {
            Guard.Argument(limit, nameof(limit)).InRange(1, TagsMaximumLimit);

            Page.Validate(page, nameof(page), TagsMaximumPage, position);

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
