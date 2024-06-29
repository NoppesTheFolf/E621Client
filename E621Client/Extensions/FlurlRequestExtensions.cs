using Flurl.Http;
using Flurl.Http.Content;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Noppes.E621.Extensions
{
    /// <summary>
    /// Adds e621 authentication functionality to the <see cref="IFlurlRequest"/> class. Also
    /// provides a way of returning null values on specified HTTP status codes.
    /// </summary>
    internal static class FlurlRequestExtensions
    {
        public static IFlurlRequest AuthenticatedIfPossible(this IFlurlRequest flurlRequest, E621Client e621Client)
        {
            return e621Client.Credentials == null
                ? flurlRequest
                : flurlRequest.Authenticated(e621Client);
        }

        public static IFlurlRequest Authenticated(this IFlurlRequest flurlRequest, E621Client e621Client)
        {
            if (!e621Client.HasLogin)
                throw E621ClientNotAuthenticatedException.Create();

#pragma warning disable 8602 // If the E621Client instance has a login, then the credentials are always available.
            return flurlRequest.WithBasicAuth(e621Client.Credentials.Username, e621Client.Credentials.ApiKey);
#pragma warning restore 8602
        }

        public delegate Task<IFlurlResponse> MakeRequest(IFlurlRequest request);

        public delegate T? DeserializeContent<out T>(IFlurlResponse response, string content) where T : class;

        public delegate T ReadToken<out T>(JToken token);

        public static Task<T> GetJsonAsync<T>(this IFlurlRequest request, ReadToken<T> readToken) where T : class
        {
            // Will never return null because no null status codes are provided
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return request.SendJsonAsync(request => request.GetAsync(), readToken, false);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public static Task<T?> GetJsonAsync<T>(this IFlurlRequest request, bool defaultIfNotJson, params HttpStatusCode[] defaultStatusCodes) where T : class
        {
            return request.SendJsonAsync(request => request.GetAsync(), token => token.ToObject<T>(), defaultIfNotJson, defaultStatusCodes);
        }

        public static Task<T?> GetJsonAsync<T>(this IFlurlRequest request, ReadToken<T> readToken, bool defaultIfNotJson, params HttpStatusCode[] defaultStatusCodes) where T : class
        {
            return request.SendJsonAsync(request => request.GetAsync(), readToken, defaultIfNotJson, defaultStatusCodes);
        }

        private static Task<T?> SendJsonAsync<T>(this IFlurlRequest request,
            MakeRequest makeRequest, ReadToken<T> readToken, bool defaultIfNotJson, params HttpStatusCode[] defaultStatusCodes) where T : class
        {
            return request.SendAsync(makeRequest, (response, content) =>
            {
                if (defaultIfNotJson && response.ResponseMessage.Content.Headers.ContentType.MediaType != "application/json")
                    return default;

                var token = JToken.Parse(content);

                return readToken(token);
            }, defaultStatusCodes);
        }

        public static Task<T?> PostMultipartAsync<T>(this IFlurlRequest request, Action<CapturedMultipartContent> buildContent, ReadToken<T> readToken, bool defaultIfNotJson, params HttpStatusCode[] defaultStatusCodes) where T : class
        {
            return request.PostMultipartAsync(request => request.PostMultipartAsync(buildContent), readToken, defaultIfNotJson, defaultStatusCodes);
        }

        private static Task<T?> PostMultipartAsync<T>(this IFlurlRequest request,
            MakeRequest makeRequest, ReadToken<T> readToken, bool defaultIfNotJson, params HttpStatusCode[] defaultStatusCodes) where T : class
        {
            return request.SendAsync(makeRequest, (response, content) =>
            {
                if (defaultIfNotJson && response.ResponseMessage.Content.Headers.ContentType.MediaType != "application/json")
                    return default;

                var token = JToken.Parse(content);

                return readToken(token);
            }, defaultStatusCodes);
        }

        private static async Task<T?> SendAsync<T>(this IFlurlRequest request,
            MakeRequest makeRequest, DeserializeContent<T> deserializeContent, params HttpStatusCode[] defaultStatusCodes) where T : class
        {
            request.AllowHttpStatus(defaultStatusCodes);

            var response = await makeRequest(request).ConfigureAwait(false);

            if (defaultStatusCodes.Contains((HttpStatusCode)response.StatusCode))
                return default;

            var content = await response.ResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            return deserializeContent(response, content);
        }

        public static IFlurlRequest AllowHttpStatus(this IFlurlRequest request, params HttpStatusCode[] statusCodes) =>
            request.AllowHttpStatus(statusCodes.Cast<int>().ToArray());
    }
}
