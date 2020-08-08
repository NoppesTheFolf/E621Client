namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts a string into a collection by splitting it at a newline character.
    /// </summary>
    internal class NewlineSplitConverter<T> : SplitConverter<T>
    {
        public NewlineSplitConverter() : base('\n')
        {
        }
    }
}
