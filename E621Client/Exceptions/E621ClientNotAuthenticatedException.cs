using System;

namespace Noppes.E621
{
    /// <summary>
    /// Thrown whenever an attempt is made to access a protected resource without there being a
    /// logged-in user.
    /// </summary>
    public class E621ClientNotAuthenticatedException : Exception
    {
        internal E621ClientNotAuthenticatedException()
        {
        }

        internal E621ClientNotAuthenticatedException(string message) : base(message)
        {
        }

        internal E621ClientNotAuthenticatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal static E621ClientNotAuthenticatedException Create() =>
            new E621ClientNotAuthenticatedException("A protected resource tried to be accessed without there being a logged-in user.");
    }
}
