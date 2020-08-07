using System;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Throttles the rate at which requests are allowed to be made.
    /// </summary>
    internal interface ILimiter
    {
        /// <summary>
        /// Executes a request and potentially waits until a requested may be made again.
        /// </summary>
        Task<T> ExecuteAsync<T>(Func<Task<T>> requestAsync, int? interval = null, int? delayAfterRequest = null);
    }
}
