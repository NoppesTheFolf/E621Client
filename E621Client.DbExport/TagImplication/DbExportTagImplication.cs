using System;

namespace Noppes.E621.DbExport
{
    /// <summary>
    /// Represents a tag implication sourced from a database export.
    /// </summary>
    public class DbExportTagImplication
    {
        /// <summary>
        /// ID of the tag implication.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the tag that implies another tag. The implied tag is stored in <see cref="ConsequentName"/>.
        /// </summary>
        public string AntecedentName { get; set; } = null!;

        /// <summary>
        /// The name of the tag implied by <see cref="AntecedentName"/>.
        /// </summary>
        public string ConsequentName { get; set; } = null!;

        /// <summary>
        /// When the tag implication got created. Null in cases where the creation date has not been
        /// saved by e621.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Status of the tag implication.
        /// </summary>
        public TagImplicationStatus Status { get; set; }
    }
}
