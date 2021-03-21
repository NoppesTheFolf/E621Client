using Flurl.Http;
using Noppes.E621.Extensions;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Client used to make requests with to the e621 API. Can only be created using the <see
    /// cref="E621ClientBuilder"/> class.
    /// </summary>
    public partial class E621Client : IE621Client, IDisposable
    {
        /// <summary>
        /// The base URL that is used to create requests with.
        /// </summary>
        public string BaseUrl { get; }

        private TimeSpan _timeout;
        /// <summary>
        /// The amount of time before a request is considered timed out.
        /// </summary>
        public TimeSpan Timeout
        {
            get => _timeout;
            internal set
            {
                _flurlClient.WithTimeout(value);

                _timeout = value;
            }
        }

        private readonly string _baseUrlRegistrableDomain;
        private readonly IFlurlClient _flurlClient;
        private readonly IRequestHandler _requestHandler;

        internal E621Client(string baseUrlRegistrableDomain, string baseUrl, E621UserAgent userAgent, TimeSpan requestInterval, int maximumConnections)
        {
            _baseUrlRegistrableDomain = baseUrlRegistrableDomain;
            BaseUrl = baseUrl;

            E621HttpClientFactory clientFactory = new E621HttpClientFactory(maximumConnections);

            _flurlClient = new FlurlClient(BaseUrl)
                .Configure(options => options.HttpClientFactory = clientFactory)
                .WithHeader("User-Agent", userAgent.ToString());

            _requestHandler = new E621RequestHandler(requestInterval);
        }

        /// <summary>
        /// Sends a GET request and returns the response body as a stream. Note that this method
        /// will act in accordance with the <see cref="BaseUrl"/> set when the client was built.
        /// Providing this method with an absolute URL will cause it to match it with the base url.
        /// If the registrable domain of the url does not match that of the base url, an exception
        /// will be thrown.
        /// </summary>
        /// <param name="url">
        /// The URL at which the resource is located of which you want the response body as a stream.
        /// </param>
        public Task<Stream> GetStreamAsync(string url)
        {
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
                throw new ArgumentException("Invalid URL.", nameof(uri));

            if (uri.IsAbsoluteUri && uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                throw new ArgumentException("Absolute URL is use neither the HTTP scheme or HTTPS scheme.");

            string relativeUrl = url;
            if (uri.IsAbsoluteUri)
            {
                var match = Regex.Match(url, "http[s]?:\\/\\/(.+?)(?=\\/|$)(.*)", RegexOptions.IgnoreCase);

                if (!match.Groups[1].Value.EndsWith(_baseUrlRegistrableDomain))
                    throw new ArgumentException("The provided URL does not match the registrable domain of the base URL.", nameof(url));
            }

            return RequestAsync(relativeUrl, request => request
                .AuthenticatedIfPossible(this)
                .GetStreamAsync());
        }

        private async Task<T> RequestAsync<T>(string urlSegment, Func<IFlurlRequest, Task<T>> func, int? interval = null, int? delayAfterRequest = null)
        {
            try
            {
                var request = _flurlClient.Request(urlSegment);

                return await _requestHandler.ExecuteAsync(() => func(request), interval, delayAfterRequest)
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw E621ClientExceptionHelper.FromException(exception);
            }
        }

        private bool IsDisposed { get; set; }

        /// <inheritdoc/>
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
                _flurlClient.Dispose();

            IsDisposed = true;
        }
    }
}
