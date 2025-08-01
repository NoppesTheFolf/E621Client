﻿using System.Collections.Generic;
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
        public Task AddFavoriteAsync(int postId);

        /// <summary>
        /// Removes a post from the user's favorites. Trying to remove a post that does
        /// not exist or is not a favorite, will silently 'fail'.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task RemoveFavoriteAsync(int postId);

        /// <summary>
        /// Gets the favorites of the user with the provided user ID. Null will be returned in case
        /// there doesn't exist a user with the given user ID. The maximum possible number of
        /// favorites which can be retrieved in a single call, is defined at <see
        /// cref="E621Constants.FavoritesMaximumLimit"/>. An empty collection will be returned
        /// if the user for which the favorites are retrieved has privacy mode enabled.
        /// </summary>
        /// <param name="userId">The ID of the user which favorites should be retrieved.</param>
        /// <param name="page">Pagination, page number.</param>
        /// <param name="limit">The maximum number of posts returned in a single request.</param>
        public Task<ICollection<Post>?> GetFavoritesAsync(int userId, int? page = null, int limit = E621Constants.FavoritesMaximumLimit);

        /// <summary>
        /// Gets the currently logged-in user's favorited posts. The maximum possible number of
        /// favorites which can be retrieved in a single call, is defined at <see cref="E621Constants.FavoritesMaximumLimit"/>.
        /// </summary>
        /// <param name="page">Pagination, page number.</param>
        /// <param name="limit">The maximum number of posts returned in a single request.</param>
        public Task<ICollection<Post>> GetOwnFavoritesAsync(int? page = null, int limit = E621Constants.FavoritesMaximumLimit);
    }
}
