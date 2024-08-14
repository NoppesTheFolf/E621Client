using Dawn;
using System;

namespace Noppes.E621
{
    /// <summary>
    /// Creates <see cref="E621Client"/> instances in a nice fluent manner.
    /// </summary>
    public class E621ClientBuilder
    {
        private E621UserAgent? UserAgent { get; set; }

        private Uri BaseUrl { get; set; } = E621Constants.DefaultBaseUrl;

        private TimeSpan RequestTimeout { get; set; } = E621Constants.RecommendedRequestTimeout;

        private TimeSpan RequestInterval { get; set; } = E621Constants.RecommendedRequestInterval;

        private int MaximumConnections { get; set; } = E621Constants.DefaultMaximumConnections;

        /// <summary>
        /// Sets the User-Agent header that is sent with each request made. This header is used to
        /// identify your application and without it you will be denied access to all resources.
        /// </summary>
        /// <param name="productName">The name of the program you are developing.</param>
        /// <param name="productVersion">The version of the program you are developing.</param>
        /// <param name="username">
        /// Your profile's username on the platform on which staff members of e621 may contact you
        /// if your application is causing trouble.
        /// </param>
        /// <param name="platform">
        /// The name of the platform on which staff members of e621 may contact you if your
        /// application is causing trouble.
        /// </param>
        /// <param name="location">A URL leading to your profile on the specified platform.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public E621ClientBuilder WithUserAgent(string productName, string productVersion, string username, string platform, string? location = null) =>
            Set(() => UserAgent = new E621UserAgent(productName, productVersion, username, platform, location));

        /// <summary>
        /// Sets the imageboard used to retrieve data from. Not specifying an imageboard will make
        /// the client use <see cref="E621Constants.DefaultImageboard"/>.
        /// </summary>
        [Obsolete("No longer supported, use the WithBaseUrl(Uri) overload instead.")]
        public E621ClientBuilder WithBaseUrl(Imageboard imageboard) => Set(() => BaseUrl = imageboard switch
        {
            Imageboard.E621 => E621Constants.E621BaseUrl,
            Imageboard.E926 => E621Constants.E926BaseUrl,
            _ => E621Constants.DefaultBaseUrl
        });

        /// <summary>
        /// Sets the image board used to retrieve data from using the base URL of the image board.
        /// </summary>
        /// <param name="baseUrl">The full absolute base URL to use the client on. For example https://e621.net</param>
        /// <exception cref="ArgumentException">If the given <paramref name="baseUrl"/> is not a valid absolute URL.</exception>
        public E621ClientBuilder WithBaseUrl(Uri baseUrl) => Set(() =>
        {
            if (!baseUrl.IsAbsoluteUri)
                throw new ArgumentException("The base URL to an image board has to be a absolute URL!", "baseUrl");
            BaseUrl = baseUrl;
        });

        /// <summary>
        /// Sets the amount of time between each request. This value has to be greater than or equal
        /// to <see cref="E621Constants.MinimumRequestInterval"/>. Not specifying the request interval
        /// will make the client use <see cref="E621Constants.RecommendedRequestInterval"/>.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public E621ClientBuilder WithRequestInterval(TimeSpan interval) =>
            Set(() =>
            {
                // Prevent requests faster than the minimum interval
                RequestInterval = Guard.Argument(interval, nameof(interval))
                    .Min(E621Constants.MinimumRequestInterval);
            });

        /// <summary>
        /// Sets the maximum amount of time a request may take to complete before an exception is
        /// thrown. This value must be higher or equal to the one defined at <see
        /// cref="E621Constants.MinimumRequestTimeout"/>. Not specifying a timeout will make the client
        /// use <see cref="E621Constants.RecommendedRequestTimeout"/>.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public E621ClientBuilder WithTimeout(TimeSpan timeout) =>
            Set(() =>
            {
                // Prevent timeouts shorter than the minimum timeout
                RequestTimeout = Guard.Argument(timeout, nameof(timeout))
                    .Min(E621Constants.MinimumRequestTimeout);
            });

        /// <summary>
        /// Sets the maximum number of connections that may be used simultaneously to communicate
        /// with the API. Must be between 1 and the limit defined in <see
        /// cref="E621Constants.MaximumConnectionsLimit"/>. Not specifying the maximum number of
        /// connection will make the client use <see cref="E621Constants.DefaultMaximumConnections"/>.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public E621ClientBuilder WithMaximumConnections(int maximumConnections) =>
            Set(() =>
            {
                MaximumConnections = Guard.Argument(maximumConnections, nameof(maximumConnections))
                    .InRange(1, E621Constants.MaximumConnectionsLimit);
            });

        private E621ClientBuilder Set(Action action)
        {
            action();

            return this;
        }

        /// <summary>
        /// Creates an instance of <see cref="E621Client"/>. An <see
        /// cref="InvalidOperationException"/> exception will be thrown if the User-Agent has not
        /// been set. A custom User-Agent header is required by the API. You can set one using the
        /// <see cref="WithUserAgent"/> method.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public IE621Client Build()
        {
            if (UserAgent == null)
                throw new InvalidOperationException($"The user agent must be specified in order to build the client.");

            IE621Client e621Client = new E621Client(BaseUrl, UserAgent, RequestInterval, MaximumConnections)
            {
                Timeout = RequestTimeout
            };

            return e621Client;
        }
    }
}