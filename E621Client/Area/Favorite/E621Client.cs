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
        /// Gets the currently logged-in user's favorited posts. The maximum possible number of
        /// favorites retrieved in a single call, is defined at <see cref="FavoritesMaximum"/>.
        /// </summary>
        /// <param name="page">Pagination, page number.</param>
        public Task<ICollection<Post>> GetFavoritesAsync(int? page = null)
        {
            Guard.Argument(page, nameof(page)).Positive();

            return CatchAsync(() =>
            {
                var request = FlurlClient
                    .Request("/favorites.json")
                    .Authenticated(this);

                if (page != null)
                {
                    request = request.SetQueryParams(new
                    {
                        page
                    });
                }

                return request.GetJsonAsync(token => token.SelectToken("posts").ToObject<ICollection<Post>>());
            });
        }
    }
}
