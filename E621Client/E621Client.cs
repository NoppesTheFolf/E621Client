using Flurl.Http;
using System;

namespace Noppes.E621
{
    /// <summary>
    /// Client used to make requests with to the e621 API. Can only be created using
    /// the <see cref="E621ClientBuilder"/> class.
    /// </summary>
    public partial class E621Client : IDisposable
    {
        private IFlurlClient FlurlClient { get; }

        internal E621Client(E621UserAgent userAgent, string baseUrl, TimeSpan requestInterval, int maximumConnections)
        {
            E621HttpClientFactory clientFactory = new E621HttpClientFactory(requestInterval, maximumConnections);

            FlurlClient = new FlurlClient(baseUrl)
                .Configure(options => options.HttpClientFactory = clientFactory)
                .WithHeader("User-Agent", userAgent.ToString());
        }

        private bool IsDisposed { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
                FlurlClient.Dispose();

            IsDisposed = true;
        }
    }
}
