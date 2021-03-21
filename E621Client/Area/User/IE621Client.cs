using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the user part of E621Client.
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