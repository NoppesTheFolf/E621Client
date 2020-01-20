using System;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <inheritdoc/>
    public class RateLimiter : ILimiter
    {
        public int Interval { get; }

        private long LastProceedTime { get; set; }

        private object InstanceLock { get; }

        public RateLimiter(TimeSpan interval)
        {
            Interval = (int)Math.Round(interval.TotalMilliseconds, MidpointRounding.AwayFromZero);
            LastProceedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - Interval;
            InstanceLock = new object();
        }

        /// <inheritdoc/>
        public async Task WaitAsync()
        {
            int waitTime = CalculateWaitTime();

            if (waitTime != 0)
                await Task.Delay(waitTime).ConfigureAwait(false);
        }

        private int CalculateWaitTime()
        {
            lock (InstanceLock)
            {
                long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                // Set lastActionUnixTime to the sum of lastActionUnixTime and delay if the sum of those values is bigger than the current unix time
                long nextProceedTime = LastProceedTime + Interval;
                int timeToWait = (int)(nextProceedTime - currentTime);
                if (nextProceedTime > currentTime)
                {
                    LastProceedTime = nextProceedTime;
                    return timeToWait;
                }

                // Set lastActionUnixTime to the current time and return a delay of 0 if none of the if statements matched
                LastProceedTime = currentTime;
                return 0;
            }
        }
    }
}
