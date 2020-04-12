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
        /// <summary>
        /// The maximum possible number of favorites retrieved in a single call to <see cref="GetFavoritesAsync"/>.
        /// </summary>
        public static int FavoritesMaximum { get; } = 75;

        private enum FavoriteAction
        {
            Add,
            Remove
        }

        /// <summary>
        /// Adds a post to the user's favorites. Trying to add a post that does
        /// not exist or is already a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task AddFavoriteAsync(Post post) =>
            AddFavoriteAsync(post.Id);

        /// <summary>
        /// Adds a post to the user's favorites. Trying to add a post that does
        /// not exist or is already a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task AddFavoriteAsync(int postId) =>
            FavoriteAsync(postId, FavoriteAction.Add);

        /// <summary>
        /// Removes a post from the user's favorites. Trying to remove a post that does
        /// not exist or is not a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task RemoveFavoriteAsync(Post post) =>
            RemoveFavoriteAsync(post.Id);

        /// <summary>
        /// Removes a post from the user's favorites. Trying to remove a post that does
        /// not exist or is not a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task RemoveFavoriteAsync(int postId) =>
            FavoriteAsync(postId, FavoriteAction.Remove);

        private Task FavoriteAsync(int postId, FavoriteAction action)
        {
            return CatchAsync(() =>
            {
                var requestUrl = action switch
                {
                    FavoriteAction.Add => "/favorites.json",
                    FavoriteAction.Remove => $"/favorites/{postId}.json",
                    _ => throw new ArgumentOutOfRangeException(nameof(action))
                };

                // UnprocessableEntity: Post is already a favorite
                var request = FlurlClient.Request(requestUrl)
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

        /// <summary>
        /// Gets the favorites of the user with the provided user ID. Null will be returned in case
        /// there doesn't exist a user with the given user ID. The maximum possible number of
        /// favorites retrieved in a single call, is defined at <see cref="FavoritesMaximum"/>.
        /// </summary>
        /// <param name="userId">The ID of the user which favorites should be retrieved.</param>
        /// <param name="page">Pagination, page number.</param>
        /// <returns></returns>
        public Task<ICollection<Post>?> GetFavoritesAsync(int userId, int? page = null) => GetFavoritesAsync((int?)userId, page);

        /// <summary>
        /// Gets the currently logged-in user's favorited posts. The maximum possible number of
        /// favorites retrieved in a single call, is defined at <see cref="FavoritesMaximum"/>.
        /// </summary>
        /// <param name="page">Pagination, page number.</param>
        public Task<ICollection<Post>> GetOwnFavoritesAsync(int? page = null)
        {
            // A logged-in user will always exist and will therefore always have a list of favorites.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return GetFavoritesAsync(null, page);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        private Task<ICollection<Post>?> GetFavoritesAsync(int? userId = null, int? page = null)
        {
            Guard.Argument(userId, nameof(userId)).Positive();
            Guard.Argument(page, nameof(page)).Positive();

            return CatchAsync(() =>
            {
                var request = FlurlClient.Request("/favorites.json");

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
    }
}
