namespace Noppes.E621
{
    /// <summary>
    /// The orders in which a listing of users can be sorted.
    /// </summary>
    public enum UserOrder
    {
        /// <summary>
        /// Order by username in an ascending order. 
        /// </summary>
        [Parameter("name")]
        Username,
        /// <summary>
        /// Order by the total number of active posts maintained by the user in descending order.
        /// </summary>
        [Parameter("posts")]
        ActivePostsCount,
        /// <summary>
        /// Order by the total number of posts deleted which the user maintained in descending order.
        /// </summary>
        [Parameter("deleted")]
        DeletedPostsCount,
        /// <summary>
        /// Order by the total number of note edits by the user in descending order.
        /// </summary>
        [Parameter("notes")]
        NoteEditsCount,
        /// <summary>
        /// Order by the total number of edits the user has made to the tags of posts in descending order.
        /// </summakry>
        [Parameter("tagedits")]
        TagEditsCount,
        /// <summary>
        /// Order by the date the user joined in descending order.
        /// </summary>
        [Parameter("date")]
        JoinDate,
        /// <summary>
        /// Order by a user their cumulative record score in descending order.
        /// </summary>
        [Parameter("record")]
        RecordScore
    }
}
