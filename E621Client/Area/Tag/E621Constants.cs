namespace Noppes.E621
{
    // Contains constants related to e621's tags area.
    public static partial class E621Constants
    {
        /// <summary>
        /// The maximum number of tags which can be retrieved in a single call to one of the
        /// GetTagsAsync overloads.
        /// </summary>
        public static readonly int TagsMaximumLimit = 1000;

        /// <summary>
        /// The maximum allowed page number when making a call one of the GetTagsAsync overloads.
        /// </summary>
        public static readonly int TagsMaximumPage = 750;
    }
}
