using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Client which can be used to make requests with to the e621 API.
    /// </summary>
    public partial interface IE621Client : IDisposable
    {
        /// <summary>
        /// The imageboard to which the requests are made.
        /// </summary>
        [Obsolete]
        public Imageboard Imageboard { get; }

        /// <summary>
        /// The base URL that is used to create requests with.
        /// </summary>
        public string BaseUrl { get; }

        /// <summary>
        /// The amount of time before a request is considered timed out.
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Sends a GET request and returns the response body as a stream. Note that this method
        /// will act in accordance with the <see cref="BaseUrl"/> set when the client was built.
        /// Providing this method with an absolute URL will cause it to match it with the base url.
        /// If the registrable domain of the url does not match that of the base url, an exception
        /// will be thrown.
        /// </summary>
        public Task<Stream> GetStreamAsync(string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead);
    }
}