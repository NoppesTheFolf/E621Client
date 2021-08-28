namespace Noppes.E621
{
    /// <summary>
    /// The order in which artists can be sorted.
    /// </summary>
    public enum ArtistOrder
    {
        /// <summary>
        /// Ascending order by an artist their name.
        /// </summary>
        [Parameter("name")]
        Name,
        /// <summary>
        /// Descending order by the creation date.
        /// </summary>
        [Parameter("created_at")]
        CreatedAt,
        /// <summary>
        /// Descending order by when the artist was last updated.
        /// </summary>
        [Parameter("updated_at")]
        UpdatedAt,
        /// <summary>
        /// Descending order by the amount of posts of which the artist is the creator.
        /// </summary>
        [Parameter("post_count")]
        PostCount
    }
}
