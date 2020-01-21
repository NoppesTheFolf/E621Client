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
        private IFlurlClient FlurlClient { get; }

        internal E621Client(string baseUrl, E621UserAgent userAgent, TimeSpan requestInterval, int maximumConnections)
        {
            E621HttpClientFactory clientFactory = new E621HttpClientFactory(requestInterval, maximumConnections);

            FlurlClient = new FlurlClient(baseUrl)
                .Configure(options => options.HttpClientFactory = clientFactory)
                .WithHeader("User-Agent", userAgent.ToString());
        }

        private static async Task CatchAsync(Func<Task> action)
        {
            await CatchAsync<bool>(async () =>
            {
                await action().ConfigureAwait(false);

                return default;
            }).ConfigureAwait(false);
        }

        private static async Task<T> CatchAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return await func().ConfigureAwait(false);
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
