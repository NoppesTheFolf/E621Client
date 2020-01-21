using Flurl.Http;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Implements https://e621.net/help/show/api#basics
    public partial class E621Client
    {
        /// <summary>
        /// Logs a user in based on their username and password. Returns whether or not the log in was successful.
        /// There are two possibilities when false is returned. The first being that the combination of username
        /// and password was simply incorrect. And the second one that the user does not have API access enabled
        /// on their account. There is no way determining which of the two is the case using the API.
        /// </summary>
        /// <param name="username">Username used for logging in.</param>
        /// <param name="password">Password used for logging in.</param>
        public async Task<bool> LogInUsingPasswordAsync(string username, string password)
        {
            if (HasLogin)
                throw E621ClientAlreadyLoggedInException.Create();

            var apiKey = await GetApiKeyAsync(username, password).ConfigureAwait(false);

            if (apiKey != null)
                Credentials = new E621Credentials(username, apiKey);

            return apiKey != null;
        }

        /// <summary>
        /// Logs the user in based on their username and API key. It will validate the provided credentials and return
        /// whether or not the log in was successful.
        /// </summary>
        /// <param name="username">Username used for logging in.</param>
        /// <param name="apiKey">API key used for logging in.</param>
        public Task<bool> LogInUsingApiKeyAsync(string username, string apiKey)
        {
            if (HasLogin)
                throw E621ClientAlreadyLoggedInException.Create();

            return CatchAsync(async () =>
            {
                var createdCredentials = new E621Credentials(username, apiKey);

                bool success;
                try
                {
                    Credentials = createdCredentials;

                    var user = await GetUserAsync();

                    success = user != null;
                }
                finally
                {
                    Credentials = null;
                }

                if (success)
                    Credentials = createdCredentials;

                return success;
            });
        }

        /// <summary>
        /// Retrieves the API key of an account using username and password authentication. Will return null
        /// if the username and password combination is incorrect or when the user doesn't have API access enabled.
        /// There is no way determining if the user has API access enabled or not using the API.
        /// </summary>
        /// <param name="username">Username used to authenticate the user with.</param>
        /// <param name="password">Password used to authenticate the user with.</param>
        public Task<string?> GetApiKeyAsync(string username, string password)
        {
            return CatchAsync(async () =>
            {
                var request = FlurlClient.Request("/user/login.json")
                    .SetQueryParams(new
                    {
                        name = username,
                        password
                    });

                LoginResult result = await request.GetJsonAsync<LoginResult>().ConfigureAwait(false);

                return result.IsSuccess ? result.ApiKey : null;
            });
        }

        /// <summary>
        /// Logs a user out.
        /// </summary>
        public void Logout() => Credentials = null;
    }
}
