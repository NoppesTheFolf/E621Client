namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts a string into a collection by splitting it at a whitespace character.
    /// </summary>
    internal class WhitespaceSplitConverter<T> : SplitConverter<T>
    {
        public WhitespaceSplitConverter() : base(' ')
        {
        }
    }
}
