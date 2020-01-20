using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Handles all requests going to e621/e926. Ensures that requests
    /// are not sent too quickly after one another.
    /// </summary>
    internal class E621ClientHandler : HttpClientHandler
    {
        private ILimiter RateLimiter { get; }

        public E621ClientHandler(TimeSpan requestInterval, int maximumConnections)
        {
            MaxConnectionsPerServer = maximumConnections;
            RateLimiter = new RateLimiter(requestInterval);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await RateLimiter.WaitAsync();

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
