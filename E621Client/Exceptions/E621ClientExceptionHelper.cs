using Flurl.Http;
using System;
using System.Net;

namespace Noppes.E621
{
    public static class E621ClientExceptionHelper
    {
        public static E621ClientException FromException(Exception exception)
        {
            if (exception is E621ClientException clientException)
                return clientException;

            if (!(exception is FlurlHttpException httpException))
                return E621ClientException.Create(exception);

            if (exception is FlurlHttpTimeoutException)
                return E621ClientTimeoutException.Create(exception);

            if (httpException.Call.Response.StatusCode == HttpStatusCode.Unauthorized)
                return E621ClientUnauthorizedException.Create(exception);

            if (httpException.Call.Response.StatusCode == HttpStatusCode.Forbidden)
                return E621ClientForbiddenException.Create(exception);

            return E621ClientException.Create(exception);
        }
    }
}
