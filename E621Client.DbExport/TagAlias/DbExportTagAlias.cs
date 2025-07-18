using System;

namespace Noppes.E621.DbExport
{
    /// <summary>
    /// Represents a tag alias sourced from a database export.
    /// </summary>
    public class DbExportTagAlias
    {
        /// <summary>
        /// ID of the tag alias.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the tag which needs to be mapped to another tag (<see cref="ConsequentName"/>).
        /// </summary>
        public string AntecedentName { get; set; } = null!;

        /// <summary>
        /// The "destination" tag. The tag which should actually be used instead of <see cref="AntecedentName"/>.
        /// </summary>
        public string ConsequentName { get; set; } = null!;

        /// <summary>
        /// When the tag alias got created. Null in cases where the creation date has not been saved
        /// by e621.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Status of the tag alias.
        /// </summary>
        public TagStatus Status { get; set; }
    }
}
