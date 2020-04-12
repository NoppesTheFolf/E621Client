using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Noppes.E621.Extensions
{
    /// <summary>
    /// Adds e621 authentication functionality to the <see cref="IFlurlRequest"/> class. Also provides a way
    /// of returning null values on specified HTTP status codes.
    /// </summary>
    internal static class FlurlRequestExtensions
    {
        public static IFlurlRequest AuthenticatedIfPossible(this IFlurlRequest flurlRequest, E621Client e621Client)
        {
            if (e621Client.Credentials == null)
                return flurlRequest;

            return flurlRequest.Authenticated(e621Client);
        }

        public static IFlurlRequest Authenticated(this IFlurlRequest flurlRequest, E621Client e621Client)
        {
            if (e621Client.Credentials == null)
                throw E621ClientNotAuthenticatedException.Create();

            return flurlRequest.WithBasicAuth(e621Client.Credentials.Username, e621Client.Credentials.ApiKey);
        }

        public delegate Task<HttpResponseMessage> MakeRequest(IFlurlRequest request);

        public delegate T DeserializeContent<out T>(string content);

        public delegate T ReadToken<out T>(JToken token);

        public static Task<T?> GetJsonAsync<T>(this IFlurlRequest request, params HttpStatusCode[] nullStatusCodes) where T : class
        {
            return request.SendJsonAsync<T>(request => request.GetAsync(), nullStatusCodes);
        }

        public static Task<T?> GetJsonAsync<T>(this IFlurlRequest request,
            ReadToken<T> readToken, params HttpStatusCode[] nullStatusCodes) where T : class
        {
            return request.SendJsonAsync(request => request.GetAsync(), readToken, nullStatusCodes);
        }

        public static Task<T> GetJsonAsync<T>(this IFlurlRequest request, ReadToken<T> readToken) where T : class
        {
            // Will never return null because no null status codes are provided
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return request.SendJsonAsync(request => request.GetAsync(), readToken);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        private static Task<T?> SendJsonAsync<T>(this IFlurlRequest request,
            MakeRequest makeRequest, params HttpStatusCode[] nullStatusCodes) where T : class
        {
            return request.SendAsync(makeRequest, JsonConvert.DeserializeObject<T>, nullStatusCodes);
        }

        private static Task<T?> SendJsonAsync<T>(this IFlurlRequest request,
            MakeRequest makeRequest, ReadToken<T> readToken, params HttpStatusCode[] nullStatusCodes) where T : class
        {
            return request.SendAsync(makeRequest, content =>
            {
                var token = JToken.Parse(content);

                return readToken(token);
            }, nullStatusCodes);
        }

        private static async Task<T?> SendAsync<T>(this IFlurlRequest request,
            MakeRequest makeRequest, DeserializeContent<T> deserializeContent, params HttpStatusCode[] defaultStatusCodes) where T : class
        {
            request.AllowHttpStatus(defaultStatusCodes);

            var response = await makeRequest(request).ConfigureAwait(false);

            if (defaultStatusCodes.Contains(response.StatusCode))
                return default;

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return deserializeContent(content);
        }
    }
}
