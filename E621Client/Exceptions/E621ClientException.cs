using System;

namespace Noppes.E621
{
    public class E621ClientException : Exception
    {
        public E621ClientException()
        {
        }

        public E621ClientException(string message) : base(message)
        {
        }

        public E621ClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal static E621ClientException Create(Exception innerException) =>
            new E621ClientException("An unknown error has occured.", innerException);
    }
}
