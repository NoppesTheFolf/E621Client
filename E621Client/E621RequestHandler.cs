using Flurl.Http;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Handles requests made to e621. Throttles the rate at which requests are allowed to be made.
    /// If, for whatever reason, the API returns a 429 Too Many Requests response, it will retry the
    /// request after waiting for the minimum allowed request interval as defined in <see cref="E621Constants.MinimumRequestInterval"/>.
    /// </summary>
    internal class E621RequestHandler : IRequestHandler
    {
        public int DefaultInterval { get; }

        private long _waitUntil;
        private readonly SemaphoreSlim _requestLock;

        public E621RequestHandler(TimeSpan defaultInterval)
        {
            DefaultInterval = (int)Math.Round(defaultInterval.TotalMilliseconds, MidpointRounding.AwayFromZero);
            _waitUntil = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - int.MaxValue / 4;
            _requestLock = new SemaphoreSlim(1, 1);
        }

        /// <inheritdoc/>
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> makeRequestAsync, int? interval = null, int? delayAfterRequest = null)
        {
            await _requestLock.WaitAsync().ConfigureAwait(false);

            try
            {
                var isFirstAttempt = true;
                while (true)
                {
                    if (isFirstAttempt)
                        await WaitAsync(interval ?? DefaultInterval).ConfigureAwait(false);
                    else
                        await Task.Delay(E621Constants.MinimumRequestInterval).ConfigureAwait(false);

                    try
                    {
                        return await makeRequestAsync().ConfigureAwait(false);
                    }
                    catch (FlurlHttpException httpException)
                    {
                        if (httpException.Call.Response?.StatusCode != (int)HttpStatusCode.TooManyRequests)
                            throw;

                        isFirstAttempt = false;
                    }
                }
            }
            finally
            {
                if (delayAfterRequest != null)
                    await Task.Delay(TimeSpan.FromMilliseconds((int)delayAfterRequest)).ConfigureAwait(false);

                _requestLock.Release();
            }
        }

        private Task WaitAsync(int interval)
        {
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            var timeToWait = (int)(_waitUntil - currentTime);
            if (timeToWait > 0)
            {
                _waitUntil = currentTime + timeToWait + interval;
                return Task.Delay(timeToWait);
            }

            _waitUntil = currentTime + interval;
            return Task.CompletedTask;
        }
    }
}
