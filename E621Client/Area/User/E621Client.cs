using Flurl.Http;
using Newtonsoft.Json;
using Noppes.E621.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Implements https://e621.net/help/show/api#users
    public partial class E621Client
    {
        /// <summary>
        /// The maximum amount of users that change be retrieved in a single call to <see cref="GetUsersAsync"/>.
        /// </summary>
        public static int MaximumUsers { get; } = 20;

        /// <summary>
        /// Retrieves a listing of users based on the given parameters. The maximum number of users possibly retrieved
        /// in a single call to this method is equal to <see cref="MaximumUsers"/>.
        /// </summary>
        /// <param name="username">The username to search for. You can and should use wildcards to match parts of the username.
        /// Not specifying any wildcards will cause this method to act like <see cref="GetUserAsync(string)"/>, which you
        /// should use in that case.</param>
        /// <param name="page">The number of the page to retrieve. Must be larger than or equal to 1.</param>
        /// <param name="order">The order in which the listing should be sorted.</param>
        /// <param name="permissionLevel">The permission level the users need to have.</param>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<User>> GetUsersAsync(string? username = null, UserOrder? order = null, UserPermissionLevel? permissionLevel = null, int page = 1)
        {
            if (page < 1)
                throw new ArgumentException("The page number must be greater than or equal to 1.");

            var request = FlurlClient.Request("/user/index.json")
                .SetQueryParams(new
                {
                    name = username,
                    page,
                    order = order?.ToApiParameter(),
                    level = permissionLevel == null ? (int?)null : (int)permissionLevel,
                });

            return await request.GetJsonAsync<List<User>>().ConfigureAwait(false);
        }

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
        /// Retrieves information about the currently logged-in user. Will throw
        /// an exception if there is no user currently logged-in.
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
