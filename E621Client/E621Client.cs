using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Client used to make requests with to the e621 API. Can only be created using
    /// the <see cref="E621ClientBuilder"/> class.
    /// </summary>
    public partial class E621Client : IDisposable
    {
        /// <summary>
        /// The base URL that is used to create requests with.
        /// </summary>
        public string BaseUrl => FlurlClient.BaseUrl;

        private TimeSpan _timeout;
        /// <summary>
        /// The amount of time before a request is considered timed out.
        /// </summary>
        public TimeSpan Timeout
        {
            get => _timeout;
            internal set
            {
                FlurlClient.WithTimeout(value);

                _timeout = value;
            }
        }

        private IFlurlClient FlurlClient { get; }

        private ILimiter RateLimiter { get; }

        internal E621Client(string baseUrl, E621UserAgent userAgent, TimeSpan requestInterval, int maximumConnections)
        {
            E621HttpClientFactory clientFactory = new E621HttpClientFactory(maximumConnections);

            FlurlClient = new FlurlClient(baseUrl)
                .Configure(options => options.HttpClientFactory = clientFactory)
                .WithHeader("User-Agent", userAgent.ToString());

            RateLimiter = new RateLimiter(requestInterval);
        }

        private async Task<T> RequestAsync<T>(string urlSegment, Func<IFlurlRequest, Task<T>> func, int? interval = null, int? delayAfterRequest = null)
        {
            try
            {
                var request = FlurlClient.Request(urlSegment);

                return await RateLimiter.ExecuteAsync(() =>
                {
                    return func(request);
                }, interval, delayAfterRequest).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw E621ClientExceptionHelper.FromException(exception);
            }
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
