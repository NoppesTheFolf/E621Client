using System;

namespace Noppes.E621
{
    /// <summary>
    /// The kinds of relative positioning used for pagination.
    /// </summary>
    public enum Position
    {
        Before,
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
