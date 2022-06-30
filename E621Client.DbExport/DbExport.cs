using System;
using System.Collections.Generic;
using System.Linq;

namespace Noppes.E621.DbExport
{
    /// <summary>
    /// The different things (posts, pools, tags, etc.) of which there is a database dump on e621.
    /// </summary>
    public enum DbExportType
    {
        /// <summary>
        /// A database export of pools.
        /// </summary>
        Pool,
        /// <summary>
        /// A database export of posts.
        /// </summary>
        Post,
        /// <summary>
        /// A database export of tag aliases.
        /// </summary>
        TagAlias,
        /// <summary>
        /// A database export of tag implications.
        /// </summary>
        TagImplication,
        /// <summary>
        /// A database export of tags.
        /// </summary>
        Tag,
        /// <summary>
        /// A database export of wiki pages.
        /// </summary>
        WikiPage
    }

    /// <summary>
    /// Contains information about a database export on e621.
    /// </summary>
    public class DbExport
    {
        /// <summary>
        /// What the database export contains.
        /// </summary>
        public DbExportType Type { get; }

        /// <summary>
        /// When the database got exported.
        /// </summary>
        public DateTime When { get; }

        /// <summary>
        /// The name of the database export file on the server.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Create a <see cref="DbExport"/>.
        /// </summary>
        public DbExport(DbExportType type, DateTime when, string fileName)
        {
            Type = type;
            When = when;
            FileName = fileName;
        }
    }

    public static class DbExportEnumerableExtensions
    {
        /// <summary>
        /// Get the latest export for the given type.
        /// </summary>
        public static DbExport Latest(this IEnumerable<DbExport> exports, DbExportType type)
        {
            return exports
                .Where(x => x.Type == type)
                .OrderByDescending(x => x.When)
                .First();
        }
    }
}
