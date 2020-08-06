using System;

namespace Noppes.E621
{
    /// <summary>
    /// Thrown whenever the credentials of the currently logged-in user got invalidated.
    /// </summary>
    public class E621ClientUnauthorizedException : Exception
    {
        internal E621ClientUnauthorizedException()
        {
        }

        internal E621ClientUnauthorizedException(string message) : base(message)
        {
        }

        internal E621ClientUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal static E621ClientUnauthorizedException Create(Exception innerException)
        {
            string message = @"
Access to the requested resource was denied. This indicates that the credentials provided by 
the user got invalidated.";

            return new E621ClientUnauthorizedException(message, innerException);
        }
    }
}
