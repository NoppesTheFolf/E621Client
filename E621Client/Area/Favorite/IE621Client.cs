using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the favorites part of the E621Client.
    public partial interface IE621Client
    {
        /// <summary>
        /// Adds a post to the user's favorites. Trying to add a post that does
        /// not exist or is already a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task AddFavoriteAsync(Post post);

        /// <summary>
        /// Adds a post to the user's favorites. Trying to add a post that does
        /// not exist or is already a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task AddFavoriteAsync(int postId);

        /// <summary>
        /// Removes a post from the user's favorites. Trying to remove a post that does
        /// not exist or is not a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task RemoveFavoriteAsync(Post post);

        /// <summary>
        /// Removes a post from the user's favorites. Trying to remove a post that does
        /// not exist or is not a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task RemoveFavoriteAsync(int postId);

        /// <summary>
        /// Gets the favorites of the user with the provided user ID. Null will be returned in case
        /// there doesn't exist a user with the given user ID. The maximum possible number of
        /// favorites retrieved in a single call, is defined at <see cref="E621Constants.FavoritesMaximum"/>.
        /// </summary>
        /// <param name="userId">The ID of the user which favorites should be retrieved.</param>
        /// <param name="page">Pagination, page number.</param>
        /// <returns></returns>
        public Task<ICollection<Post>?> GetFavoritesAsync(int userId, int? page = null);

        /// <summary>
        /// Gets the currently logged-in user's favorited posts. The maximum possible number of
        /// favorites retrieved in a single call, is defined at <see cref="E621Constants.FavoritesMaximum"/>.
        /// </summary>
        /// <param name="page">Pagination, page number.</param>
        public Task<ICollection<Post>> GetOwnFavoritesAsync(int? page = null);
    }
}