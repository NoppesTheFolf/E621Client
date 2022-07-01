using System;
using System.Collections.Generic;

namespace Noppes.E621
{
    /// <summary>
    /// Represents a pool sourced from a database export.
    /// </summary>
    public class DbExportPool
    {
        /// <summary>
        /// The ID of the pool.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name given to the pool.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// When the pool was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When the pool was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// ID of the user who created the pool.
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Description of the pool. Null if there is no description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Whether or not the pool is still actively being updated with new posts.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Category of the pool.
        /// </summary>
        public PoolCategory Category { get; set; }

        /// <summary>
        /// IDs of the posts in the pool. Keep in mind that posts in a pool can be ordered and
        /// therefore this is an ordered collection.
        /// </summary>
        public IList<int> PostIds { get; set; } = null!;
    }
}
