namespace Noppes.E621
{
    /// <summary>
    /// The status of a tag alias.
    /// </summary>
    public enum TagAliasStatus
    {
        Approved = 1,
        Active = 2,
        Pending = 3,
        Deleted = 4,
        Retired = 5,
        Processing = 6,
        Queued = 7,
        /// <summary>
        /// While developing this library, the database export of tag aliases contained records with
        /// the status "error: cannot update a new record". For handle this scenario, the <see
        /// cref="Other"/> enum was created as a fallback enum.
        /// </summary>
        Other = 8
    }
}
