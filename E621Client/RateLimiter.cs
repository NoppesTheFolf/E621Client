using Flurl.Http;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Throttles the rate at which requests are allowed to be made.
    /// </summary>
    internal class RateLimiter : ILimiter
    {
        public int DefaultInterval { get; }

        private long WaitUntil { get; set; }

        private SemaphoreSlim RequestLock { get; }

        public RateLimiter(TimeSpan defaultInterval)
        {
            DefaultInterval = (int)Math.Round(defaultInterval.TotalMilliseconds, MidpointRounding.AwayFromZero);
            WaitUntil = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - int.MaxValue / 4;
            RequestLock = new SemaphoreSlim(1, 1);
        }

        private Task WaitAsync(int interval)
        {
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            var timeToWait = (int)(WaitUntil - currentTime);
            if (timeToWait > 0)
            {
                WaitUntil = currentTime + timeToWait + interval;
                return Task.Delay(timeToWait);
            }

            WaitUntil = currentTime + interval;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task<T> ExecuteAsync<T>(Func<Task<T>> requestAsync, int? interval = null, int? delayAfterRequest = null)
        {
            await RequestLock.WaitAsync();

            try
            {
                var isFirstAttempt = true;
                while (true)
                {
                    if (isFirstAttempt)
                    {
                        await WaitAsync(interval ?? DefaultInterval).ConfigureAwait(false);
                    }
                    else
                    {
                        await Task.Delay(E621Client.MinimumRequestInterval);
                    }

                    try
                    {
                        return await requestAsync().ConfigureAwait(false);
                    }
                    catch (FlurlHttpException httpException)
                    {
                        if (httpException.Call.Response?.StatusCode != HttpStatusCode.TooManyRequests)
                            throw;

                        isFirstAttempt = false;
                    }
                }
            }
            finally
            {
                if (delayAfterRequest != null)
                    await Task.Delay(TimeSpan.FromMilliseconds((int)delayAfterRequest));

                RequestLock.Release();
            }
        }
    }
}
