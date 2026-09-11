namespace Noppes.E621
{
    // Contains constants related to e621's posts area.
    public static partial class E621Constants
    {
        /// <summary>
        /// The maximum number of posts which can be retrieved in a single call to IE621Client.GetPostsAsync.
        /// </summary>
        public const int PostsMaximumLimit = 320;

        /// <summary>
        /// The maximum allowed page number when making a call to IE621Client.GetPostsAsync.
        /// </summary>
        public const int PostsMaximumPage = 750;
    }
}
