namespace Noppes.E621
{
    /// <summary>
    /// The status of a tag alias or tag implication.
    /// </summary>
    public enum TagStatus
    {
        /// <summary>
        /// Approved and eligible to be applied.
        /// </summary>
        Approved = 1,
        /// <summary>
        /// Applied to posts and currently in effect.
        /// </summary>
        Active = 2,
        /// <summary>
        /// Suggested but awaiting approval.
        /// </summary>
        Pending = 3,
        /// <summary>
        /// Rejected or removed.
        /// </summary>
        Deleted = 4,
        /// <summary>
        /// Previously active but retired.
        /// </summary>
        Retired = 5,
        /// <summary>
        /// Currently being applied.
        /// </summary>
        Processing = 6,
        /// <summary>
        /// Approved and queued to be applied.
        /// </summary>
        Queued = 7,
        /// <summary>
        /// While developing this library, the database export of tag aliases contained records with
        /// the status "error: cannot update a new record". For handle this scenario, the <see
        /// cref="Other"/> enum was created as a fallback enum.
        /// </summary>
        Other = 8
    }
}
