using System;

namespace Noppes.E621
{
    /// <summary>
    /// The kinds of relative positioning used for pagination.
    /// </summary>
    public enum Position
    {
        /// <summary>
        /// Retrieve entities positioned before the given ID.
        /// </summary>
        Before,
        /// <summary>
        /// Retrieve entities positioned after the given ID.
        /// </summary>
        After
    }

    internal static class PositionExtensions
    {
        public static char ToIdentifier(this Position position)
        {
            return position switch
            {
                Position.Before => 'b',
                Position.After => 'a',
                _ => throw new ArgumentOutOfRangeException(nameof(position))
            };
        }
    }
}
