using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the pools part of the E621Client.
    public partial interface IE621Client
    {
        /// <summary>
        /// Retrieve a pool by its ID. The return value will be null if there exists no pool with
        /// the provided ID.
        /// </summary>
        /// <param name="id">ID of pool for which we want to retrieve information.</param>
        public Task<Pool?> GetPoolAsync(int id);

        /// <summary>
        /// Retrieves a collection of pools based on the given parameters. By default, e621 returns
        /// pools in descending order of when they were last updated.
        /// <para>
        /// To narrow down the results, you can filter both on the name and description of the
        /// pools. For example, you can get all of the pools that end in the word cat by passing
        /// "*cat" to the <paramref name="name"/> parameter.
        /// </para>
        /// <para>
        /// If you're interested in a specific list of pool IDs, then you can pass these to the
        /// <paramref name="ids"/> parameter. The IDs that don't exist, will be left out of the
        /// response (so no exception will be thrown).
        /// </para>
        /// <para>
        /// You can also get the pools created by a specific user by either filtering based on their
        /// username or their user ID. In case you opt for the username approach, you won't have to
        /// worry about case sensitivity: its case insensitive.
        /// </para>
        /// <para>
        /// At last you can also filter based on whether or not be pools are deleted, active and to
        /// which category the belong.
        /// </para>
        /// <para>All of the above mentioned methods of filtering can be applied at the same time.</para>
        /// <para>
        /// You can decide how many results should be returned by e621 based on the <paramref
        /// name="limit"/> parameter. Just be aware that there is a limit on the number of pools
        /// which can be retrieved with a single call. This limit is defined at <see
        /// cref="E621Constants.PoolsMaximumLimit"/>. Exceeding this limit, will cause an exception
        /// to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the pools by specifying both a pool ID and a position. The
        /// position parameter defines the position of the returned pools relative to the given pool
        /// ID. Let's take pool with ID 1000 as an example. Passing this ID in combination with <see
        /// cref="Position.Before"/> will cause the pool 999, 998, 997, etc. to be retrieved. Using
        /// <see cref="Position.After"/> will retrieve the pools 1001, 1002, 1003, etc. You should
        /// use this method if you don't need pagination or need to avoid the limit pagination comes
        /// with it. Moreover, this is the most efficient way to navigate through pools.
        /// </para>
        /// </summary>
        /// <param name="id">
        /// ID of the pool you want the retrieved pool the be relatively positioned to.
        /// </param>
        /// <param name="position">Relative position to the given pool ID.</param>
        /// <param name="name">The text that should be included in names of the retrieved pools.</param>
        /// <param name="description">
        /// The text that should be included in the descriptions of the retrieved pools.
        /// </param>
        /// <param name="ids">The IDs of the pools you want to retrieve.</param>
        /// <param name="creatorId">The ID of the user who created the pools.</param>
        /// <param name="creatorName">The name of the user who created the pools.</param>
        /// <param name="isActive">Whether or not a pool should be active.</param>
        /// <param name="isDeleted">
        /// Whether only deleted or non-deleted pools should be retrieved. Leaving this parameter to
        /// null, will cause e621 to return both deleted and non-deleted pools.
        /// </param>
        /// <param name="category">The category to which the retrieved pools should belong.</param>
        /// <param name="limit">The number of pools to retrieve in a single call.</param>
        /// <returns></returns>
        public Task<ICollection<Pool>> GetPoolsAsync(
            int id,
            Position position,
            string? name = null,
            string? description = null,
            IEnumerable<int>? ids = null,
            int? creatorId = null,
            string? creatorName = null,
            bool? isActive = null,
            bool? isDeleted = null,
            PoolCategory? category = null,
            int? limit = null
        );

        /// <summary>
        /// Retrieves a collection of pools based on the given parameters. By default, e621 returns
        /// pools in descending order of when they were last updated.
        /// <para>
        /// To narrow down the results, you can filter both on the name and description of the
        /// pools. For example, you can get all of the pools that end in the word cat by passing
        /// "*cat" to the <paramref name="name"/> parameter.
        /// </para>
        /// <para>
        /// If you're interested in a specific list of pool IDs, then you can pass these to the
        /// <paramref name="ids"/> parameter. The IDs that don't exist, will be left out of the
        /// response (so no exception will be thrown).
        /// </para>
        /// <para>
        /// You can also get the pools created by a specific user by either filtering based on their
        /// username or their user ID. In case you opt for the username approach, you won't have to
        /// worry about case sensitivity: its case insensitive.
        /// </para>
        /// <para>
        /// At last you can also filter based on whether or not be pools are deleted, active and to
        /// which category the belong.
        /// </para>
        /// <para>All of the above mentioned methods of filtering can be applied at the same time.</para>
        /// <para>
        /// You can decide how many results should be returned by e621 based on the <paramref
        /// name="limit"/> parameter. Just be aware that there is a limit on the number of pools
        /// which can be retrieved with a single call. This limit is defined at <see
        /// cref="E621Constants.PoolsMaximumLimit"/>. Exceeding this limit, will cause an exception
        /// to be thrown.
        /// </para>
        /// <para>
        /// You can navigate through all the pools by specifying a page number. There is, however, a
        /// limit to the value. This limit is defined in <see
        /// cref="E621Constants.PoolsMaximumPage"/>. Exceeding this limit, will cause an exception
        /// to be thrown.
        /// </para>
        /// </summary>
        /// <param name="page">Pagination, number of the page.</param>
        /// <param name="name">The text that should be included in names of the retrieved pools.</param>
        /// <param name="description">
        /// The text that should be included in the descriptions of the retrieved pools.
        /// </param>
        /// <param name="ids">The IDs of the pools you want to retrieve.</param>
        /// <param name="creatorId">The ID of the user who created the pools.</param>
        /// <param name="creatorName">The name of the user who created the pools.</param>
        /// <param name="isActive">Whether or not a pool should be active.</param>
        /// <param name="isDeleted">
        /// Whether only deleted or non-deleted pools should be retrieved. Leaving this parameter to
        /// null, will cause e621 to return both deleted and non-deleted pools.
        /// </param>
        /// <param name="category">The category to which the retrieved pools should belong.</param>
        /// <param name="order">How the retrieved pools should be ordered.</param>
        /// <param name="limit">The number of pools to retrieve in a single call.</param>
        /// <returns></returns>
        public Task<ICollection<Pool>> GetPoolsAsync(
            int page = 1,
            string? name = null,
            string? description = null,
            IEnumerable<int>? ids = null,
            int? creatorId = null,
            string? creatorName = null,
            bool? isActive = null,
            bool? isDeleted = null,
            PoolCategory? category = null,
            PoolOrder order = PoolOrder.UpdatedAt,
            int? limit = null
        );
    }
}
