namespace Noppes.E621
{
    // Contains constants related to e621's artists area.
    public static partial class E621Constants
    {
        /// <summary>
        /// The maximum number of artists which can be retrieved in a single call to <see cref="IE621Client.GetArtistsAsync"/>.
        /// </summary>
        public const int ArtistsMaximumLimit = 320;

        /// <summary>
        /// The maximum allowed page number when making a call to <see cref="IE621Client.GetArtistsAsync"/>.
        /// </summary>
        public const int ArtistsMaximumPage = 750;
    }
}
