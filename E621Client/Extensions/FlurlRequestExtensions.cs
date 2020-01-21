using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Noppes.E621.Extensions
{
    /// <summary>
    /// Adds e621 authentication functionality to the <see cref="IFlurlRequest"/> class.
    /// </summary>
    internal static class FlurlRequestExtensions
    {
        public static Task<HttpResponseMessage> AuthenticatedPostUrlEncodedAsync(this IFlurlRequest flurlRequest, E621Client e621Client, object values) =>
            Authenticated(e621Client, values, parameters => flurlRequest.PostUrlEncodedAsync(parameters));

        public static IFlurlRequest AuthenticatedSetQueryParams(this IFlurlRequest flurlRequest, E621Client e621Client, object values) =>
            Authenticated(e621Client, values, parameters => flurlRequest.SetQueryParams(parameters));

        private static T Authenticated<T>(E621Client e621Client, object values, Func<IDictionary<string, object>, T> func)
        {
            var parameters = values.ToDictionary();

            AddCredentialsToParameters(e621Client, parameters);

            return func(parameters);
        }

        private static void AddCredentialsToParameters(E621Client e621Client, IDictionary<string, object> parameters)
        {
            if (e621Client.Credentials == null)
                throw E621ClientNotAuthenticatedException.Create();

            parameters.Add("login", e621Client.Credentials.Username);
            parameters.Add("password_hash", e621Client.Credentials.ApiKey);
        }
    }
}
