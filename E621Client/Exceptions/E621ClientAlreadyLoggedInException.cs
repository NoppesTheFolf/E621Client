using System;

namespace Noppes.E621
{
    /// <summary>
    /// Thrown when a login tries to take place while another user is already logged-in.
    /// </summary>
    internal class E621ClientAlreadyLoggedInException : E621ClientException
    {
        public E621ClientAlreadyLoggedInException()
        {
        }

        public E621ClientAlreadyLoggedInException(string message) : base(message)
        {
        }

        public E621ClientAlreadyLoggedInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal static E621ClientAlreadyLoggedInException Create() => 
            new E621ClientAlreadyLoggedInException("A log in tried to take place while another user is already logged in. You need to log out first.");
    }
}
