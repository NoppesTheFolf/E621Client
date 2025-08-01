namespace Noppes.E621
{
    // Contains constants related to e621's tags area.
    public static partial class E621Constants
    {
        /// <summary>
        /// The maximum number of tags which can be retrieved in a single call to one of the
        /// GetTagsAsync overloads.
        /// </summary>
        public const int TagsMaximumLimit = 320;

        /// <summary>
        /// The maximum allowed page number when making a call one of the GetTagsAsync overloads.
        /// </summary>
        public const int TagsMaximumPage = 750;
    }
}
