using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the artists area of e621
    public partial interface IE621Client
    {
        /// <summary>
        /// Retrieves a collection of artists based on the given parameters. By default, e621 will
        /// return the artists in the descending order they've been created at.
        /// </summary>
        /// <param name="page">Pagination, number of the page.</param>
        /// <param name="name">
        /// Name of the artist. May contain wildcard characters. If no wildcard characters are used,
        /// it will behave like *{name}*.
        /// </param>
        /// <param name="url">
        /// Url where the artist can be found. May contain wildcard characters. If no wildcard
        /// characters are used, it will behave like *{url}*.
        /// </param>
        /// <param name="creatorName">Name of the user that created the artist entities.</param>
        /// <param name="isActive">Whether or not the artist is active.</param>
        /// <param name="isBanned">Whether or not the artist is banned from e621.</param>
        /// <param name="hasTag">Whether or not the artist name is also the name of a tag.</param>
        /// <param name="order">The order in which the retrieved artists should be.</param>
        /// <param name="limit">The number of artists to retrieve in a single call.</param>
        public Task<ICollection<Artist>> GetArtistsAsync(
            int page = 1,
            string? name = null,
            string? url = null,
            string? creatorName = null,
            bool? isActive = null,
            bool? isBanned = null,
            bool? hasTag = null,
            ArtistOrder order = ArtistOrder.CreatedAt,
            int? limit = null
        );

        /// <summary>
        /// Retrieves a collection of artists based on the given parameters. By default, e621 will
        /// return the artists in the descending order they've been created at.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="name">
        /// Name of the artist. May contain wildcard characters. If no wildcard characters are used,
        /// it will behave like *{name}*.
        /// </param>
        /// <param name="url">
        /// Url where the artist can be found. May contain wildcard characters. If no wildcard
        /// characters are used, it will behave like *{url}*.
        /// </param>
        /// <param name="creatorName">Name of the user that created the artist entities.</param>
        /// <param name="isActive">Whether or not the artist is active.</param>
        /// <param name="isBanned">Whether or not the artist is banned from e621.</param>
        /// <param name="hasTag">Whether or not the artist name is also the name of a tag.</param>
        /// <param name="order">The order in which the retrieved artists should be.</param>
        /// <param name="limit">The number of artists to retrieve in a single call.</param>
        public Task<ICollection<Artist>> GetArtistsAsync(
            int id,
            Position position,
            string? name = null,
            string? url = null,
            string? creatorName = null,
            bool? isActive = null,
            bool? isBanned = null,
            bool? hasTag = null,
            ArtistOrder order = ArtistOrder.CreatedAt,
            int? limit = null
        );

        /// <summary>
        /// Get info about an artist by their ID. If there exists no artist with the given ID, null
        /// will be returned.
        /// </summary>
        /// <param name="id">ID of the artist to retrieve.</param>
        /// <returns></returns>
        public Task<Artist?> GetArtistAsync(int id);
    }
}
