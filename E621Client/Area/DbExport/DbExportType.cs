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
        public const string Artist = "artists";

        /// <summary>
        /// A database export of bulk update requests.
        /// </summary>
        public const string BulkUpdateRequest = "bulk_update_requests";

        /// <summary>
        /// A database export of pools.
        /// </summary>
        public const string Pool = "pools";

        /// <summary>
        /// A database export of posts.
        /// </summary>
        public const string Post = "posts";

        /// <summary>
        /// A database export of post replacements.
        /// </summary>
        public const string PostReplacement = "post_replacements";

        /// <summary>
        /// A database export of post versions.
        /// </summary>
        public const string PostVersion = "post_versions";

        /// <summary>
        /// A database export of tags.
        /// </summary>
        public const string Tag = "tags";

        /// <summary>
        /// A database export of tag aliases.
        /// </summary>
        public const string TagAlias = "tag_aliases";

        /// <summary>
        /// A database export of tag implications.
        /// </summary>
        public const string TagImplication = "tag_implications";

        /// <summary>
        /// A database export of wiki pages.
        /// </summary>
        public const string WikiPage = "wiki_pages";
    }
}
