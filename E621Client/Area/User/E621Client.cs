using Flurl.Http;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Noppes.E621.Extensions;

namespace Noppes.E621
{
    // Implements https://e621.net/help/show/api#users
    public partial class E621Client
    {
        /// <summary>
        /// Retrieve a user based on their ID. Returns null in case no user
        /// exists with the given ID.
        /// </summary>
        public Task<User?> GetUserAsync(int id) => GetUserAsync(new
        {
            id
        });

        /// <summary>
        /// Retrieve a user based on their username. Returns null in case no user
        /// exists with the given username.
        /// </summary>
        public Task<User?> GetUserAsync(string username) => GetUserAsync(new
        {
            name = username
        });

        private Task<User?> GetUserAsync(object values)
        {
            var request = FlurlClient.Request()
                .SetQueryParams(values);

            return GetUserAsync(request);
        }

        /// <summary>
        /// Retrieves information about the currently logged-in user.
        /// </summary>
        /// <exception cref="E621ClientNotAuthenticatedException"></exception>
        public Task<User> GetUserAsync()
        {
            var request = FlurlClient.Request()
                .AuthenticatedSetQueryParams(this, new { });

            // Will never return null values because the logged-in user will always exist
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return GetUserAsync(request);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        private static async Task<User?> GetUserAsync(IFlurlRequest flurlRequest)
        {
            var response = await flurlRequest
                .AppendPathSegment("/user/show.json")
                .AllowHttpStatus(HttpStatusCode.Redirect) // The API redirects instead of returning a 404 if a user does not exist...
                .GetAsync().ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Redirect)
                return null;

            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<User>(content);
        }
    }
}
