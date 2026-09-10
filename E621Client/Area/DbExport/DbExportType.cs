namespace Noppes.E621
{
    /// <summary>
    /// The names of the different database exports which are available. Use these to
    /// compare against <see cref="DbExport.Name"/>.
    /// </summary>
    public static class DbExportType
    {
        /// <summary>
        /// A database export of artists.
        /// </summary>
        public static readonly string Artist = "artists";

        /// <summary>
        /// A database export of bulk update requests.
        /// </summary>
        public static readonly string BulkUpdateRequest = "bulk_update_requests";

        /// <summary>
        /// A database export of pools.
        /// </summary>
        public static readonly string Pool = "pools";

        /// <summary>
        /// A database export of posts.
        /// </summary>
        public static readonly string Post = "posts";

        /// <summary>
        /// A database export of post replacements.
        /// </summary>
        public static readonly string PostReplacement = "post_replacements";

        /// <summary>
        /// A database export of post versions.
        /// </summary>
        public static readonly string PostVersion = "post_versions";

        /// <summary>
        /// A database export of tags.
        /// </summary>
        public static readonly string Tag = "tags";

        /// <summary>
        /// A database export of tag aliases.
        /// </summary>
        public static readonly string TagAlias = "tag_aliases";

        /// <summary>
        /// A database export of tag implications.
        /// </summary>
        public static readonly string TagImplication = "tag_implications";

        /// <summary>
        /// A database export of wiki pages.
        /// </summary>
        public static readonly string WikiPage = "wiki_pages";
    }
}
