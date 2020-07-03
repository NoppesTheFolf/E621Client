namespace Noppes.E621
{
    /// <summary>
    /// The orders in which tags can be sorted.
    /// </summary>
    public enum TagOrder
    {
        /// <summary>
        /// Order by the date the tag was created.
        /// </summary>
        [Parameter("date")]
        Date,
        /// <summary>
        /// Order by the number of posts that use the tag.
        /// </summary>
        [Parameter("count")]
        Count,
        /// <summary>
        /// Order by the name of the tag.
        /// </summary>
        [Parameter("name")]
        Name
    }
}
