using Dawn;
using Flurl.Http;
using Noppes.E621.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Noppes.E621
{
    public partial class E621Client
    {
        /// <inheritdoc cref="IE621Client.GetPostsAsync(int,Noppes.E621.Position,System.Nullable{int},string?)"/>
        public Task<ICollection<Post>> GetPostsAsync(int id, Position position, int? limit = null, string? tags = null) => GetPostsAsync(tags, limit, id, position);

        /// <inheritdoc cref="IE621Client.GetPostsAsync(int,Noppes.E621.Position,System.Nullable{int},string?)"/>
        public Task<ICollection<Post>> GetPostsAsync(string? tags = null, int? page = null, int? limit = null) => GetPostsAsync(tags, limit, page, null);

        /// <inheritdoc cref="IE621Client.GetPostAsync(int)"/>
        public Task<Post?> GetPostAsync(int id)
        {
            return GetPostAsync($"/posts/{id}.json", null);
        }

        /// <inheritdoc cref="IE621Client.GetPostAsync(string)"/>
        public Task<Post?> GetPostAsync(string md5)
        {
            Guard.Argument(md5, nameof(md5)).Length(32);

            return GetPostAsync("/posts.json", new
            {
                md5 = md5.ToLowerInvariant()
            });
        }

        private Task<ICollection<Post>> GetPostsAsync(string? tags, int? limit, int? page, Position? position)
        {
            Guard.Argument(limit, nameof(limit)).InRange(1, E621Constants.PostsMaximumLimit);

            Page.Validate(page, nameof(page), E621Constants.PostsMaximumPage, position);

            var splitTags = TagHelper.ParseSearchString(tags);

            if (splitTags.Length > E621Constants.PostsMaximumTagSearchCount)
                throw new ArgumentException($"Only {E621Constants.PostsMaximumTagSearchCount} tags can be searched for at once. You provided {splitTags.Length} tags.");

            return RequestAsync("/posts.json", request =>
                request.SetQueryParams(new
                    {
                        limit,
                        tags = splitTags.Length == 0 ? null : string.Join(' ', splitTags)
                    })
                    .SetPagination(page, position)
                    .AuthenticatedIfPossible(this)
                    .GetJsonAsync(token => token["posts"].ToObject<ICollection<Post>>()));
        }

        private Task<Post?> GetPostAsync(string url, object? queryParameters)
        {
            return RequestAsync(url, request =>
            {
                request = request.AuthenticatedIfPossible(this);

                if (queryParameters != null)
                    request = request.SetQueryParams(queryParameters);

                return request.GetJsonAsync(token =>
                {
                    return token.SelectToken("post").ToObject<Post>();
                }, true, HttpStatusCode.NotFound);
            });
        }
    }
}