using System;

namespace Noppes.E621
{
    public class E621ClientUnauthorizedException : E621ClientException
    {
        public E621ClientUnauthorizedException()
        {
        }

        public E621ClientUnauthorizedException(string message) : base(message)
        {
        }

        public E621ClientUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal new static E621ClientUnauthorizedException Create(Exception innerException)
        {
            string message = @"
Access to the requested resource was denied. This may indicate the user doesn't have the 
permission to access the resource in question. It may also be that the used User-Agent has 
been blocked by e621, but this is rather unlikely.";

            return new E621ClientUnauthorizedException(message, innerException);
        }
    }
}
