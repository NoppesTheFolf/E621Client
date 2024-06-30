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
        public Task AddFavoriteAsync(int postId)
        {
            return RequestAsync("/favorites.json", request =>
            {
                request = request
                    .AllowHttpStatus(HttpStatusCode.UnprocessableEntity) // UnprocessableEntity: Post is already a favorite
                    .Authenticated(this);

                return request.PostJsonAsync(new
                {
                    post_id = postId
                });
            });
        }

        /// <inheritdoc/>
        public Task RemoveFavoriteAsync(int postId)
        {
            return RequestAsync($"/favorites/{postId}.json", request =>
            {
                request = request.Authenticated(this);

                return request.DeleteAsync();
            });
        }

        /// <inheritdoc/>
        public Task<ICollection<Post>?> GetFavoritesAsync(int userId, int? page = null, int? limit = null) => InternalGetFavoritesAsync(userId, page, limit);

        /// <inheritdoc/>
        public Task<ICollection<Post>> GetOwnFavoritesAsync(int? page = null, int? limit = null)
        {
            // A logged-in user will always exist and will therefore always have a list of favorites.
            return InternalGetFavoritesAsync(null, page, limit)!;
        }

        private Task<ICollection<Post>?> InternalGetFavoritesAsync(int? userId, int? page, int? limit)
        {
            Guard.Argument(userId, nameof(userId)).Positive();
            Guard.Argument(page, nameof(page)).Positive();
            Guard.Argument(limit, nameof(limit)).InRange(1, E621Constants.FavoritesMaximumLimit);

            return RequestAsync("/favorites.json", request =>
            {
                if (userId == null)
                {
                    request = request.Authenticated(this);
                }

                request = request
                    .SetQueryParam("user_id", userId)
                    .SetQueryParam("page", page)
                    .SetQueryParam("limit", limit);

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
