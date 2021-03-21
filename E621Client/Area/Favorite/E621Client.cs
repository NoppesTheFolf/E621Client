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
        /// <inheritdoc/>
        public Task AddFavoriteAsync(Post post) =>
            AddFavoriteAsync(post.Id);

        /// <inheritdoc/>
        public Task AddFavoriteAsync(int postId) =>
            FavoriteAsync(postId, FavoriteAction.Add);

        /// <inheritdoc/>
        public Task RemoveFavoriteAsync(Post post) =>
            RemoveFavoriteAsync(post.Id);

        /// <inheritdoc/>
        public Task RemoveFavoriteAsync(int postId) =>
            FavoriteAsync(postId, FavoriteAction.Remove);

        /// <inheritdoc/>
        public Task<ICollection<Post>?> GetFavoritesAsync(int userId, int? page = null) => GetFavoritesAsync((int?)userId, page);

        /// <inheritdoc/>
        public Task<ICollection<Post>> GetOwnFavoritesAsync(int? page = null)
        {
            // A logged-in user will always exist and will therefore always have a list of favorites.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return GetFavoritesAsync(page: page);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        private Task FavoriteAsync(int postId, FavoriteAction action)
        {
            var requestUrl = action switch
            {
                FavoriteAction.Add => "/favorites.json",
                FavoriteAction.Remove => $"/favorites/{postId}.json",
                _ => throw new ArgumentOutOfRangeException(nameof(action))
            };

            return RequestAsync(requestUrl, request =>
            {
                // UnprocessableEntity: Post is already a favorite
                request = request
                    .AllowHttpStatus(HttpStatusCode.UnprocessableEntity)
                    .Authenticated(this);

                return action switch
                {
                    FavoriteAction.Remove => request.DeleteAsync(),
                    FavoriteAction.Add => request.PostJsonAsync(new
                    {
                        post_id = postId
                    }),
                    _ => throw new ArgumentOutOfRangeException(nameof(action))
                };
            });
        }

        private Task<ICollection<Post>?> GetFavoritesAsync(int? userId = null, int? page = null)
        {
            Guard.Argument(userId, nameof(userId)).Positive();
            Guard.Argument(page, nameof(page)).Positive();

            return RequestAsync("/favorites.json", request =>
            {
                if (userId == null)
                {
                    request = request.Authenticated(this);
                }

                request = request.SetQueryParams(new
                {
                    page,
                    user_id = userId
                });

                // DefaultIfNotJson: e621 returns an 200 OK page with HTML saying "Not Found" when the given user doesn't exist.
                // NotFound: Future proofing in case e621 replaces the 200 OK page with an appropriate 404 Not Found JSON response
                return request.GetJsonAsync(token =>
                {
                    return token.SelectToken("posts").ToObject<ICollection<Post>>();
                }, true, HttpStatusCode.NotFound);
            });
        }

        private enum FavoriteAction
        {
            Add,
            Remove
        }
    }
}