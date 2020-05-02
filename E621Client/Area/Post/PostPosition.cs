using System;

namespace Noppes.E621
{
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
