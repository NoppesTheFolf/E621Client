using System;

namespace Noppes.E621
{
    /// <summary>
    /// Thrown whenever a string parameter is null or consists only out of whitespace characters.
    /// </summary>
    public class ArgumentNullOrWhiteSpaceException : ArgumentException
    {
        public ArgumentNullOrWhiteSpaceException(string paramName)
            : base($"The parameter \"{paramName}\" may not be null or consist only out of whitespace characters.", paramName)
        {
        }
    }
}
