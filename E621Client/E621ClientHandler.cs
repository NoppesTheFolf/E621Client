using System.Net.Http;

namespace Noppes.E621
{
    /// <summary>
    /// Handles all requests going to e621. Ensures that requests
    /// are not sent too quickly after one another.
    /// </summary>
    internal class E621ClientHandler : HttpClientHandler
    {
        public E621ClientHandler(int maximumConnections)
        {
            MaxConnectionsPerServer = maximumConnections;
            AllowAutoRedirect = false;
        }
    }
}
