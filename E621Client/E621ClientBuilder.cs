using Noppes.E621.Extensions;
using System;

namespace Noppes.E621
{
    /// <summary>
    /// Creates <see cref="E621Client"/> instances in a nice fluent manner.
    /// </summary>
    public class E621ClientBuilder
    {
        private E621UserAgent? UserAgent { get; set; }

        private string _baseUrl = E621Client.DefaultImageboard.AsBaseUrl();
        private string BaseUrl
        {
            get => _baseUrl;
            set
            {
                Uri uri = new Uri(value, UriKind.Absolute);
                uri.EnsureHttpOrHttps();

                _baseUrl = value;
            }
        }

        private TimeSpan RequestTimeout { get; set; } = E621Client.RecommendedRequestTimeout;

        private TimeSpan RequestInterval { get; set; } = E621Client.RecommendedRequestInterval;

        private int MaximumConnections { get; set; } = E621Client.DefaultMaximumConnections;

        /// <summary>
        /// Sets the User-Agent header that is sent with each request made. This header
        /// is used to identify your application and without it you will be denied access
        /// to all resources.
        /// </summary>
        /// <param name="productName">The name of the program you are developing.</param>
        /// <param name="productVersion">The version of the program you are developing.</param>
        /// <param name="username">Your profile's username on the platform on which staff members of e621
        /// may contact you if your application is causing trouble.</param>
        /// <param name="platform">The name of the platform on which staff members of e621
        /// may contact you if your application is causing trouble.</param>
        /// <param name="location">A URL leading to your profile on the specified platform.</param>
        public E621ClientBuilder WithUserAgent(string productName, string productVersion, string username, string platform, string? location = null) =>
            Set(() => UserAgent = new E621UserAgent(productName, productVersion, username, platform, location));

        /// <summary>
        /// Sets the imageboard used to retrieve data from. Not specifying an imageboard
        /// will make the client use <see cref="E621Client.DefaultImageboard"/>.
        /// </summary>
        public E621ClientBuilder WithBaseUrl(Imageboard imageboard) =>
            Set(() => BaseUrl = imageboard.AsBaseUrl());

        /// <summary>
        /// Sets the amount of time between each request. This value has to be greater than or equal to
        /// <see cref="E621Client.MinimumRequestInterval"/>. Not specifying the request interval will
        /// make the client use <see cref="E621Client.RecommendedRequestInterval"/>.
        /// </summary>
        public E621ClientBuilder WithRequestInterval(TimeSpan interval) =>
            Set(() =>
            {
                // Prevent requests faster than the minimum interval
                if (interval < E621Client.MinimumRequestInterval)
                    throw new ArgumentOutOfRangeException(nameof(interval),
                        $"The interval between each request must be greater than or equal to {E621Client.MinimumRequestInterval.TotalMilliseconds} ms.");

                RequestInterval = interval;
            });

        /// <summary>
        /// Sets the maximum amount of time a request may take to complete before an exception is thrown. This value
        /// must be higher or equal to the one defined at <see cref="E621Client.MinimumRequestTimeout"/>. Not specifying
        /// a timeout will make the client use <see cref="E621Client.RecommendedRequestTimeout"/>.
        /// </summary>
        public E621ClientBuilder WithTimeout(TimeSpan timeout) =>
            Set(() =>
            {
                if (timeout < E621Client.MinimumRequestTimeout)
                    throw new ArgumentOutOfRangeException(nameof(timeout),
                        $"The timeout must be at least {E621Client.MinimumRequestTimeout.TotalSeconds} seconds long.");

                RequestTimeout = timeout;
            });

        /// <summary>
        /// Sets the maximum number of connections that may be used simultaneously to communicate with the API.
        /// Must be between 1 and the limit defined in <see cref="E621Client.MaximumConnectionsLimit"/>. Not specifying
        /// the maximum number of connection will make the client use <see cref="E621Client.DefaultMaximumConnections"/>.
        /// </summary>
        public E621ClientBuilder WithMaximumConnections(int maximumConnections) =>
            Set(() =>
            {
                if (maximumConnections < 1 || maximumConnections > E621Client.MaximumConnectionsLimit)
                    throw new ArgumentOutOfRangeException(nameof(maximumConnections),
                        $"Must be between 1 and {E621Client.MaximumConnectionsLimit}.");

                MaximumConnections = maximumConnections;
            });

        /// <summary>
        /// Sets the base URL that should be used when making requests. You probably do not
        /// want to use this method. Look at <see cref="WithBaseUrl(Imageboard)"/> first.
        /// </summary>
        public E621ClientBuilder WithBaseUrl(string baseUrl) =>
            Set(() => BaseUrl = baseUrl);

        private E621ClientBuilder Set(Action action)
        {
            action();

            return this;
        }

        /// <summary>
        /// Creates an instance of <see cref="E621Client"/>. An <see cref="InvalidOperationException"/>
        /// exception will be thrown if the User-Agent has not been set. A custom User-Agent header
        /// is required by the API. You can set one using the <see cref="WithUserAgent"/> method.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public E621Client Build()
        {
            if (UserAgent == null)
                throw new InvalidOperationException($"The user agent must be specified in order to build the client.");

            E621Client e621Client = new E621Client(BaseUrl, UserAgent, RequestInterval, MaximumConnections)
            {
                Timeout = RequestTimeout
            };

            return e621Client;
        }
    }
}
