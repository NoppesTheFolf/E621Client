using System;

namespace Noppes.E621
{
    public class E621ClientNotAuthenticatedException : E621ClientException
    {
        public E621ClientNotAuthenticatedException()
        {
        }

        public E621ClientNotAuthenticatedException(string message) : base(message)
        {
        }

        public E621ClientNotAuthenticatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal static E621ClientNotAuthenticatedException Create() =>
            new E621ClientNotAuthenticatedException("A protected resource tried to be accessed without there being a logged-in user.");
    }
}
