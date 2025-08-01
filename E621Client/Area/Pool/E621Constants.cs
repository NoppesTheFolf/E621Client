namespace Noppes.E621
{
    // Contains constants related to e621's pools area.
    public static partial class E621Constants
    {
        /// <summary>
        /// The maximum number of pools which can be retrieved in a single call to <see cref="IE621Client.GetPoolsAsync"/>.
        /// </summary>
        public const int PoolsMaximumLimit = 320;

        /// <summary>
        /// The maximum allowed page number when making a call to <see cref="IE621Client.GetPoolsAsync"/>.
        /// </summary>
        public const int PoolsMaximumPage = 750;
    }
}
