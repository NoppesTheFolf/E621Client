using Flurl.Http;
using Flurl.Http.Newtonsoft;
using Noppes.E621.Extensions;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Client used to make requests with to the e621 API. Can only be created using the <see
    /// cref="E621ClientBuilder"/> class.
    /// </summary>
    public partial class E621Client : IE621Client
    {
        /// <inheritdoc/>
        public string BaseUrl { get; }

        private TimeSpan _timeout;
        /// <inheritdoc/>
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
        private readonly string _userAgent;
        private readonly IFlurlClient _flurlClient;
        private readonly E621RequestHandler _requestHandler;

        internal E621Client(Uri imageboard, E621UserAgent userAgent, TimeSpan requestInterval, int maximumConnections)
        {
            BaseUrl = imageboard.AbsoluteUri;
            _baseUrlRegistrableDomain = imageboard.Host;

            var httpClientHandler = new E621ClientHandler(maximumConnections);
            var httpClient = new HttpClient(httpClientHandler);

            _flurlClient = new FlurlClient(httpClient, BaseUrl)
                .WithSettings(settings =>
                {
                    settings.JsonSerializer = new NewtonsoftJsonSerializer();
                });

            _userAgent = userAgent.ToString();

            _requestHandler = new E621RequestHandler(requestInterval);
        }

        /// <inheritdoc/>
        public async Task<Stream> GetStreamAsync(string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
                throw new ArgumentException("Invalid URL.", nameof(uri));

            if (uri.IsAbsoluteUri && uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                throw new ArgumentException("Absolute URL is use neither the HTTP scheme or HTTPS scheme.");

            if (uri.IsAbsoluteUri)
            {
                var match = Regex.Match(url, "http[s]?:\\/\\/(.+?)(?=\\/|$)(.*)", RegexOptions.IgnoreCase);

                if (!match.Groups[1].Value.EndsWith(_baseUrlRegistrableDomain))
                    throw new ArgumentException("The provided URL does not match the registrable domain of the base URL.", nameof(url));
            }

            var response = await RequestAsync(url, request => request
                .AuthenticatedIfPossible(this)
                .GetAsync(completionOption: completionOption))
                .ConfigureAwait(false);

            return await response.GetStreamAsync().ConfigureAwait(false);
        }

        private async Task<T> RequestAsync<T>(string urlSegment, Func<IFlurlRequest, Task<T>> func, int? interval = null, int? delayAfterRequest = null)
        {
            try
            {
                var request = _flurlClient.Request(urlSegment)
                    .WithHeader("User-Agent", _userAgent);

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
            {
                _requestHandler.Dispose();
                _flurlClient.Dispose();
            }

            IsDisposed = true;
        }
    }
}