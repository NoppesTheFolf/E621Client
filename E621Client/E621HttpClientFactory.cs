using Flurl.Http.Configuration;
using System.Net.Http;

namespace Noppes.E621
{
    /// <summary>
    /// Creates <see cref="HttpClient"/> instances which use <see cref="E621ClientHandler"/> for
    /// their requests.
    /// </summary>
    internal class E621HttpClientFactory : DefaultHttpClientFactory
    {
        private readonly int _maximumConnection;

        public E621HttpClientFactory(int maximumConnection)
        {
            _maximumConnection = maximumConnection;
        }

        public override HttpMessageHandler CreateMessageHandler() =>
            new E621ClientHandler(_maximumConnection);
    }
}
