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
Access to the requested resource was denied. This indicates that the credentials provided by 
the user got invalidated.";

            return new E621ClientUnauthorizedException(message, innerException);
        }
    }
}
