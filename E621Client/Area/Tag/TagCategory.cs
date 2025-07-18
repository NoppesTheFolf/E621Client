namespace Noppes.E621
{
    /// <summary>
    /// All of the categories a tag can be identified as.
    /// </summary>
    public enum TagCategory
    {
        /// <summary>
        /// The general category is for all the tags that do not match the description of all the
        /// other categories.
        /// </summary>
        General = 0,
        /// <summary>
        /// Tags in the artist category identify the artist(s) of a post.
        /// </summary>
        Artist = 1,
        /// <summary>
        /// Tags in the copyright category are used for any post which contains a program or series
        /// a copyrighted character or other element was first featured in
        /// </summary>
        Copyright = 3,
        /// <summary>
        /// Tags in the character category define the name(s) of a character/the characters.
        /// </summary>
        Character = 4,
        /// <summary>
        /// Tags in the species category describes the species of a character/the characters shown
        /// in the post.
        /// </summary>
        Species = 5,
        /// <summary>
        /// Tags in the invalid category are not allowed on any posts. This can for example be
        /// because they describe things that are too common or unspecific (e.g. colors).
        /// </summary>
        Invalid = 6,
        /// <summary>
        /// Tags in the meta category are used to give information about the technical side of the
        /// post's file, the post itself, or things relating to e621's own handling of a post.
        /// </summary>
        Meta = 7,
        /// <summary>
        /// Tags in the lore category provide additional information that "tag what you see" doesn't cover.
        /// </summary>
        Lore = 8
    }
}
