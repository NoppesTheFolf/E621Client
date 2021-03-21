namespace Noppes.E621
{
    // Contains constants related to e621's posts area.
    public static partial class E621Constants
    {
        /// <summary>
        /// The maximum number of posts which can be retrieved in a single call to <see cref="IE621Client.GetPostsAsync"/>.
        /// </summary>
        public static readonly int PostsMaximumLimit = 320;

        /// <summary>
        /// The maximum allowed page number when making a call to <see cref="IE621Client.GetPostsAsync"/>.
        /// </summary>
        public static readonly int PostsMaximumPage = 750;

        /// <summary>
        /// The maximum number of tags which can be searched for in a single call to <see cref="IE621Client.GetPostsAsync"/>.
        /// </summary>
        public static readonly int PostsMaximumTagSearchCount = 6;
    }
}
