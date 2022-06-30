namespace Noppes.E621.DbExport
{
    /// <summary>
    /// Represents a tag sourced from a database export.
    /// </summary>
    public class DbExportTag
    {
        /// <summary>
        /// ID of the tag.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the tag.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The category the tag belongs in.
        /// </summary>
        public TagCategory Category { get; set; }

        /// <summary>
        /// The number of posts that make use of the tag.
        /// </summary>
        public int Count { get; set; }
    }
}
