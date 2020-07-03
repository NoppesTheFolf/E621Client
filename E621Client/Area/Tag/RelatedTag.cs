namespace Noppes.E621
{
    /// <summary>
    /// Represents a tag that is related to some other tag.
    /// </summary>
    public class RelatedTag
    {
        /// <summary>
        /// Name of the related tag.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// "Weight" of the related tag. The larger the number, the stronger the connection.
        /// </summary>
        public int Weight { get; set; }
    }
}
