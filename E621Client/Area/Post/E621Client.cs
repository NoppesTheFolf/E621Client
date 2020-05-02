using Dawn;
using Flurl.Http;
using Noppes.E621.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noppes.E621
{
    public partial class E621Client
    {
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
        /// Retrieves a collection of posts based on the given parameters.
        /// <para>
        /// You can choose which posts to show and hide by filtering on tags. The maximum number of
        /// tags that can be used, is defined at <see cref="PostsMaximumTagSearchCount"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// Be aware that there is a limit on the number of posts which can be retrieved with a
        /// single call. This limit is defined at <see cref="PostsMaximumLimit"/>. Exceeding this
        /// limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the posts by specifying both an post ID and a position. The
        /// position parameter defines the position of the returned posts relative to the given post
        /// ID. Let's take post with ID 1000 as an example. Passing this ID in combination with <see
        /// cref="Position.Before"/> will cause the posts 999, 998, 997, etc. to be retrieved. Using
        /// <see cref="Position.After"/> will retrieve the posts 1001, 1002, 1003, etc. You should
        /// use this method if you don't need pagination or need to avoid the limit pagination
        /// comes with it. Moreover, this is the most efficient way to navigate through posts.
        /// </para>
        /// </summary>
        /// <param name="tags">Tags used to filter the result.</param>
        /// <param name="limit">The maximum number of posts returned in a single request.</param>
        /// <param name="id">ID of the post you want the retrieved posts the be relatively positioned to.</param>
        /// <param name="position">Relative position to the given post ID.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="E621ClientTimeoutException"></exception>
        /// <exception cref="E621ClientException"></exception>
        public Task<ICollection<Post>> GetPostsAsync(int id, Position position, int? limit = null, string? tags = null) => GetPostsAsync(tags, limit, id, position);

        /// <summary>
        /// Retrieves a collection of posts based on the given parameters.
        /// <para>
        /// You can choose which posts to show and hide by filtering on tags. The maximum number of
        /// tags that can be used, is defined at <see cref="PostsMaximumTagSearchCount"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// Be aware that there is a limit on the number of posts which can be retrieved with a
        /// single call. This limit is defined at <see cref="PostsMaximumLimit"/>. Exceeding this
        /// limit, will cause an exception to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the posts by specifying a page number. There is, however, a
        /// limit to the value. This limit is defined in <see cref="PostsMaximumPage"/>. Exceeding
        /// this limit, will cause an exception to be thrown.
        /// </para>
        /// </summary>
        /// <param name="tags">Tags used to filter the result.</param>
        /// <param name="limit">The maximum number of posts returned in a single request.</param>
        /// <param name="page">Pagination, number of the page.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="E621ClientTimeoutException"></exception>
        /// <exception cref="E621ClientException"></exception>
        public Task<ICollection<Post>> GetPostsAsync(string? tags = null, int? page = null, int? limit = null) => GetPostsAsync(tags, limit, page, null);

        private Task<ICollection<Post>> GetPostsAsync(string? tags, int? limit, int? page, Position? position)
        {
            Guard.Argument(limit, nameof(limit)).InRange(1, PostsMaximumLimit);

            Guard.Argument(page, nameof(page)).InRange(1, PostsMaximumPage);

            var splitTags = TagHelper.ParseSearchString(tags);

            if (splitTags.Length > PostsMaximumTagSearchCount)
                throw new ArgumentException($"Only {PostsMaximumTagSearchCount} tags can be searched for at once. You provided {splitTags.Length} tags.");

            return CatchAsync(() =>
            {
                return FlurlClient.Request("/posts.json")
                    .SetQueryParams(new
                    {
                        limit,
                        page = page == null ? null : position == null ? page.ToString() : ((Position)position).ToIdentifier() + page.ToString(),
                        tags = splitTags.Length == 0 ? null : string.Join(' ', splitTags)
                    })
                    .AuthenticatedIfPossible(this)
                    .GetJsonAsync(token => token["posts"].ToObject<ICollection<Post>>());
            });
        }

        /// <summary>
        /// Retrieves the post with the given ID. A null value will be returned if there doesn't
        /// exist a post with the given ID.
        /// </summary>
        /// <exception cref="E621ClientTimeoutException"></exception>
        /// <exception cref="E621ClientException"></exception>
        public async Task<Post?> GetPostAsync(int id)
        {
            return await GetPostAsync($"/posts/{id}.json", null);
        }

        /// <summary>
        /// Retrieves the post of which the image its MD5 hash matches the given MD5 hash. A null
        /// value will be returned if no image matches the given hash or if the post attached to the
        /// image has been deleted.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="E621ClientTimeoutException"></exception>
        /// <exception cref="E621ClientException"></exception>
        public Task<Post?> GetPostAsync(string md5)
        {
            Guard.Argument(md5, nameof(md5)).Length(32);

            return GetPostAsync("/posts.json", new
            {
                md5 = md5.ToLowerInvariant()
            });
        }

        private Task<Post?> GetPostAsync(string url, object? values = null)
        {
            return CatchAsync(() =>
            {
                var request = FlurlClient.Request(url);

                if (values != null)
                    request = request.SetQueryParams(values);

                return request.GetJsonAsync(token => token.SelectToken("post").ToObject<Post>(), true);
            });
        }
    }
}
