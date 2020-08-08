using Flurl.Http;
using Noppes.E621.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noppes.E621
{
    public partial class E621Client
    {
        /// <summary>
        /// Gets information about the user with the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        public async Task<User?> GetUserAsync(string username)
        {
            username = username.Trim();

            var result = await RequestAsync("/users.json", request =>
            {
                return request
                    .AuthenticatedIfPossible(this)
                    .SetQueryParam("search[name_matches]", username)
                    .GetJsonAsync<IList<User>>();
            });

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Gets information about the currently logged-in user.
        /// </summary>
        public async Task<User> GetLoggedInUserAsync()
        {
            if (!HasLogin)
                throw E621ClientNotAuthenticatedException.Create();

#pragma warning disable CS8602 // Dereference of a possibly null reference. Credentials will never be null because there is a user is logged-in
#pragma warning disable 8603 // It is impossible for the method to return a null value because a logged-in user always exists
            return await GetUserAsync(Credentials.Username);
#pragma warning restore 8603
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
