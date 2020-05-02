using System;

namespace Noppes.E621
{
    class E621ClientForbiddenException : E621ClientException
    {
        public E621ClientForbiddenException()
        {
        }

        public E621ClientForbiddenException(string message) : base(message)
        {
        }

        public E621ClientForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal new static E621ClientForbiddenException Create(Exception innerException)
        {
            string message = @"
Access to the requested resource was denied. This may indicate the user doesn't have the 
permission to access the resource in question. It may also be that the used User-Agent has 
been blocked by e621, but this is rather unlikely.";

            return new E621ClientForbiddenException(message, innerException);
        }
    }
}
