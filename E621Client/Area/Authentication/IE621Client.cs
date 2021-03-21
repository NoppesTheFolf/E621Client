using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the authentication part of the E621Client.
    public partial interface IE621Client
    {
        /// <summary>
        /// Whether or not a user is currently logged in.
        /// </summary>
        public bool HasLogin { get; }

        /// <summary>
        /// Logs the user in based on their username and API key. It will validate the provided credentials and return
        /// whether or not the log in was successful or not.
        /// </summary>
        /// <param name="username">Username used for logging in.</param>
        /// <param name="apiKey">API key used for logging in.</param>
        public Task<bool> LogInAsync(string username, string apiKey);

        /// <summary>
        /// Logs a user out.
        /// </summary>
        public void Logout();
    }
}