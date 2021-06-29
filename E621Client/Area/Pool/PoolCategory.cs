namespace Noppes.E621
{
    /// <summary>
    /// Categories a pool can be belong to. A pool category says something about what the
    /// relationship is between the posts in the pool.
    /// </summary>
    public enum PoolCategory
    {
        /// <summary>
        /// Pools belonging to the <see cref="Series"/> category are part of a series, such as a comic.
        /// </summary>
        [Parameter("series")]
        Series = 1,
        /// <summary>
        /// Pools belonging to the <see cref="Collection"/> category are pools in which the posts
        /// share a common, subjective theme.
        /// </summary>
        [Parameter("collection")]
        Collection = 2
    }
}
