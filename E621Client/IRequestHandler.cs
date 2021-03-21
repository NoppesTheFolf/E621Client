using System;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Handles HTTP requests.
    /// </summary>
    internal interface IRequestHandler
    {
        /// <summary>
        /// Executes a request.
        /// </summary>
        Task<T> ExecuteAsync<T>(Func<Task<T>> makeRequestAsync, int? interval = null, int? delayAfterRequest = null);
    }
}
