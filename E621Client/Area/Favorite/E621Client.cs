using Flurl.Http;
using Newtonsoft.Json.Linq;
using Noppes.E621.Extensions;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Implements https://e621.net/help/show/api#favorites
    public partial class E621Client
    {
        /// <summary>
        /// Adds a post to the user's favorites. Trying to add a post that does
        /// not exist or is already a favorite, will silently fail.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task CreateFavoriteAsync(Post post) =>
            CreateFavoriteAsync(post.Id);

        /// <summary>
        /// Adds a post to the user's favorites. Trying to add a post that does
        /// not exist or is already a favorite, will silently fail.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task CreateFavoriteAsync(int postId) =>
            FavoriteAsync(postId, FavoriteAction.Create);

        /// <summary>
        /// Removes a post from the user's favorites. Trying to remove a post that does
        /// not exist or is not a favorite, will silently fail.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task DestroyFavoriteAsync(Post post) =>
            DestroyFavoriteAsync(post.Id);

        /// <summary>
        /// Removes a post from the user's favorites. Trying to remove a post that does
        /// not exist or is not a favorite, will silently fail.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task DestroyFavoriteAsync(int postId) =>
            FavoriteAsync(postId, FavoriteAction.Destroy);

        private Task FavoriteAsync(int postId, FavoriteAction action)
        {
            return CatchAsync(() =>
            {
                var requestUrl = action switch
                {
                    FavoriteAction.Create => "/favorite/create.json",
                    FavoriteAction.Destroy => "/favorite/destroy.json",
                    _ => throw new ArgumentOutOfRangeException(nameof(action))
                };

                // Locked: Post is already a favorite
                // InternalServerError: Post does not exist
                return FlurlClient.Request(requestUrl)
                    .SetQueryParams(new
                    {
                        id = postId.ToString(CultureInfo.InvariantCulture)
                    })
                    .AllowHttpStatus(HttpStatusCode.Locked, HttpStatusCode.InternalServerError)
                    .PostAuthenticatedUrlEncodedAsync(this, new
                    {
                        id = postId.ToString(CultureInfo.InvariantCulture)
                    });
            });
        }

        private enum FavoriteAction
        {
            Create,
            Destroy
        }

        /// <summary>
        /// Retrieve an array of usernames of users who favorited the post. Will return null
        /// if the given post does not exist. The usernames can still be retrieved from
        /// deleted posts.
        /// </summary>
        public Task<string[]?> GetUsersWhoFavoritedPostAsync(Post post) =>
            GetUsersWhoFavoritedPostAsync(post.Id);

        /// <summary>
        /// Retrieve an array of usernames of users who favorited the post. Will return null
        /// if the given post does not exist. The usernames can still be retrieved from
        /// deleted posts.
        /// </summary>
        public Task<string[]?> GetUsersWhoFavoritedPostAsync(int postId)
        {
            return CatchAsync(() =>
            {
                return FlurlClient.Request("/favorite/list_users.json")
                    .SetQueryParams(new
                    {
                        id = postId
                    })
                    .GetJsonAsync(token =>
                    {
                        var usersCsv = token["favorited_users"].Value<string>();

                        return string.IsNullOrWhiteSpace(usersCsv)
                            ? Array.Empty<string>()
                            : usersCsv.Split(',');
                    }, HttpStatusCode.NotFound);
            });
        }
    }
}
