using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Throttles the rate at which requests are allowed to be made.
    /// </summary>
    internal interface ILimiter
    {
        /// <summary>
        /// Wait until a request is allowed to made. 
        /// </summary>
        Task WaitAsync();
    }
}
