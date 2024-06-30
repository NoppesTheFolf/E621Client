namespace Noppes.E621
{
    // Contains constants related to e621's favorites area.
    public static partial class E621Constants
    {
        /// <summary>
        /// The maximum possible number of favorites retrieved in a single call to <see cref="IE621Client.GetFavoritesAsync"/> or <see cref="IE621Client.GetOwnFavoritesAsync"/>.
        /// </summary>
        public static readonly int FavoritesMaximumLimit = 320;
    }
}
