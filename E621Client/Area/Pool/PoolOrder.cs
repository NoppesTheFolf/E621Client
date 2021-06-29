namespace Noppes.E621
{
    /// <summary>
    /// The order in which pools can be sorted.
    /// </summary>
    public enum PoolOrder
    {
        /// <summary>
        /// Ascending order by name.
        /// </summary>
        [Parameter("name")]
        Name,
        /// <summary>
        /// Descending order by the creation date.
        /// </summary>
        [Parameter("created_at")]
        CreatedAt,
        /// <summary>
        /// Descending order by the update date.
        /// </summary>
        [Parameter("updated_at")]
        UpdatedAt,
        /// <summary>
        /// Descending order by the amount of posts contained in a pool.
        /// </summary>
        [Parameter("post_count")]
        PostCount
    }
}
