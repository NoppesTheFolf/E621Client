using System;

namespace Noppes.E621
{
    public class E621ClientTimeoutException : E621ClientException
    {
        public E621ClientTimeoutException()
        {
        }

        public E621ClientTimeoutException(string message) : base(message)
        {
        }

        public E621ClientTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal new static E621ClientTimeoutException Create(Exception innerException) =>
            new E621ClientTimeoutException("The request timed out.", innerException);
    }
}
