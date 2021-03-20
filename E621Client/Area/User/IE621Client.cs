using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Interface for the User part of E621Client.
    /// </summary>
    public partial interface IE621Client
    {
        /// <summary>
        /// Gets information about the user with the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        public Task<User?> GetUserAsync(string username);

        /// <summary>
        /// Gets information about the currently logged-in user.
        /// </summary>
        public Task<User> GetLoggedInUserAsync();
    }
}