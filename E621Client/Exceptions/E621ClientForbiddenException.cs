using System;

namespace Noppes.E621
{
    /// <summary>
    /// Thrown when an attempt was made to access a resource the user doesn't have the permissions
    /// to view/use. Can also indicate that the used User-Agent has been blacklisted.
    /// </summary>
    public class E621ClientForbiddenException : Exception
    {
        internal E621ClientForbiddenException()
        {
        }

        internal E621ClientForbiddenException(string message) : base(message)
        {
        }

        internal E621ClientForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal static E621ClientForbiddenException Create(Exception innerException)
        {
            string message = @"
Access to the requested resource was denied. This may indicate the user doesn't have the 
permission to access the resource in question. It may also be that the used User-Agent has 
been blocked by e621, but this is rather unlikely.";

            return new E621ClientForbiddenException(message, innerException);
        }
    }
}
